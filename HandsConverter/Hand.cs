﻿using System.Collections.Generic;
using HandsConverter.Converters;

namespace HandsConverter
{
	public class Hand
    {
        public long tournamentNumber;
        public int arabicLevel;
        public int smallBlind;
        public int bigBlind;
        public int ante;
        public int maxSeat;
        public int numberOfPlayers;
        public int sidePotNumber;
        public Dictionary<string, long> playersPutInAmount;// need to store how many chips player has made
        private List<string> hand;

        private bool SHOULD_CONVERT = false;
        public Hand(List<string> hand)
        {
            this.hand = hand;
            sidePotNumber = 1;
        }
	    #region Party methods
		public void InitializeParty()
        {
            var players = Helper.PartyPlayers;
            playersPutInAmount = new Dictionary<string, long>(); 
            numberOfPlayers = 0;
            ante = 0;
            foreach (var line in hand)
            {
                var headerConvereter = new HandHeaderConverter(line, ante);
                if (headerConvereter.IsMatch())
                {
                    tournamentNumber = headerConvereter.tournamentNumber;
                    arabicLevel = headerConvereter.arabicLevel;
                    smallBlind = headerConvereter.smallBlind;
                    bigBlind = headerConvereter.bigBlind;
                    continue;
                }
                var seatPreview = new SeatPreviewConverter(line);
                if (seatPreview.IsMatch())
                {
                    playersPutInAmount.Add(seatPreview.playerName, 0);//store how many chips each player put-in on each street
                    numberOfPlayers++;
                    if(SHOULD_CONVERT == false)
                        if (players.ContainsKey(seatPreview.playerName))
                            SHOULD_CONVERT = true;
                    continue;
                }
                var postAnte = new PostAnteConverter(line);
                if (postAnte.IsMatch())
                {
                    ante = postAnte.ante;
                    break;
                }
            }
        }
	   

		public List<string> ToParty()
        {
            InitializeParty();
            if (SHOULD_CONVERT == false) return null; // return null - if not need to convert
            var result = new List<string>();

            var uncalledBetPlayers = new Dictionary<string, long>(); //need to add uncalled bet to win chips count

            var seatPreviewCount = 0;// or after seatPreview rows
            var state = Enums.State.PreFlop;


            foreach (var line in hand)
            {
                var uncalledBetConv = new UncalledBetConverter(line);
                if (uncalledBetConv.IsMatch())
                {
                    uncalledBetPlayers.Add(uncalledBetConv.playerName, uncalledBetConv.uncalledBet);
                    continue;
                }

                var collectedPotConv = new CollectedPotConverter(line, sidePotNumber, playersPutInAmount);
                if (collectedPotConv.IsMatch())
                {
                    result.Add(collectedPotConv.ConvertToParty(uncalledBetPlayers));
                    sidePotNumber = collectedPotConv.sidePotNumber;
                    playersPutInAmount = collectedPotConv.playersPutInAmount;
                    if (uncalledBetPlayers.ContainsKey(collectedPotConv.playerName))
                        uncalledBetPlayers.Remove(collectedPotConv.playerName);
                    continue;
                }

                #region FLOP TURN RIVER Converters and reset playersPutInAmount after each street
                var flopConv = new FlopConverter(line);
                if(flopConv.IsMatch())
                {
                    state = Enums.State.Flop;
                    ResetPlayersPutInCollection();
                    result.Add(flopConv.ConvertToParty());
                    continue;
                }

                var turnConv = new TurnConverter(line);
                if (turnConv.IsMatch())
                {
                    state = Enums.State.Turn;
                    ResetPlayersPutInCollection();
                    result.Add(turnConv.ConvertToParty());
                    continue;
                }

                var riverConv = new RiverConverter(line);
                if (riverConv.IsMatch())
                {
                    state = Enums.State.River;
                    ResetPlayersPutInCollection();
                    result.Add(riverConv.ConvertToParty());
                    continue;
                }
                #endregion
                   
                var postBBConv =  new PostBBConverter(line, playersPutInAmount);
                if(postBBConv.IsMatch())
                {
                    result.Add(postBBConv.ConvertToParty());
                    playersPutInAmount = postBBConv.playersPutInAmount;
                    continue;
                }

                var postSBConv = new PostSBConverter(line, playersPutInAmount);
                if (postSBConv.IsMatch())
                {
                    result.Add(postSBConv.ConvertToParty());
                    playersPutInAmount = postSBConv.playersPutInAmount;
                    continue;
                }

                var betConv = new BetConverter(line, playersPutInAmount);
                if (betConv.IsMatch())
                {
                    result.Add(betConv.ConvertToParty());
                    playersPutInAmount = betConv.playersPutInAmount;
                    continue;
                }

                var callConv = new CallConverter(line, playersPutInAmount);
                if (callConv.IsMatch())
                {
                    result.Add(callConv.ConvertToParty());
                    playersPutInAmount = callConv.playersPutInAmount;
                    continue;
                }

                var raiseConv = new RaiseConverter(line, playersPutInAmount);
                if (raiseConv.IsMatch())
                {
                    result.Add(raiseConv.ConvertToParty());
                    playersPutInAmount = raiseConv.playersPutInAmount;
                    continue;
                }

                //Add two aditional rows befor post ante or after seat preview
                var seatPrevConv = new SeatPreviewConverter(line);
                if(seatPrevConv.IsMatch())
                {
                    result.Add(seatPrevConv.ConvertToParty());
                    seatPreviewCount++;
                    if (seatPreviewCount == numberOfPlayers)
                    {
                        result.Add($@"Trny:{tournamentNumber.ToString()} Level:{arabicLevel.ToString()} ");
                        if (ante != 0)
                            result.Add($@"Blinds-Antes({smallBlind.ToSeparateString().Trim()}/{
			                            bigBlind.ToSeparateString().Trim()
		                            } -{ante.ToSeparateString().Trim()})");
                        else
                            result.Add($@"Blinds-Antes({smallBlind.ToSeparateString().Trim()}/{
			                            bigBlind.ToSeparateString().Trim()
		                            })");
                    }
                    continue;
                }

                var converters = new List<Converter>()
                {
                    new CheckConverter(line),
                    new PostAnteConverter(line),
                    new HoleCardsConverter(line),
                    new DealtToPlayerConverter(line),
                    new PlayerShowsConverter(line),
                    new MuckHandConverter(line),
                    new HandHeaderConverter(line, ante),
                    new TableHeaderConverter(line, numberOfPlayers),
                    new FoldConverter(line)
                };

                foreach (var converter in converters)
                {
                    if (converter.IsMatch())
                    {
                        result.Add(converter.ConvertToParty());
                        break;
                    }
                }
            }

            foreach(var pl in uncalledBetPlayers)//in the end add not used uncalled bet players strings
            {
                var players = Helper.PartyPlayers;
                var convertedPlayerName = players.ContainsKey(pl.Key)
                   ? players[pl.Key]
                   : pl.Key.GetHashCode().ToString();

                result.Add($"{convertedPlayerName} wins {pl.Value.ToCommaSeparateString()} chips");
            }
            return result;
        }

        private void ResetPlayersPutInCollection()
        {
            var newCollection = new Dictionary<string, long>();
            foreach (var pair in playersPutInAmount)
                newCollection.Add(pair.Key, 0);
            playersPutInAmount = newCollection;
        }
		#endregion

		#region 888 Methods
	    public void Initialize888()
	    {
		    var players = Helper.Players888;
		    playersPutInAmount = new Dictionary<string, long>();
		    numberOfPlayers = 0;
		    ante = 0;
		    foreach (var line in hand)
		    {
			    var headerConvereter = new HandHeaderConverter(line, ante);
			    if (headerConvereter.IsMatch())
			    {
				    tournamentNumber = headerConvereter.tournamentNumber;
				    arabicLevel = headerConvereter.arabicLevel;
				    smallBlind = headerConvereter.smallBlind;
				    bigBlind = headerConvereter.bigBlind;
				    continue;
			    }
			    var seatPreview = new SeatPreviewConverter(line);
			    if (seatPreview.IsMatch())
			    {
				    playersPutInAmount.Add(seatPreview.playerName, 0);//store how many chips each player put-in on each street
				    numberOfPlayers++;
				    if (SHOULD_CONVERT == false)
					    if (players.ContainsKey(seatPreview.playerName))
						    SHOULD_CONVERT = true;
				    continue;
			    }
			    var postAnte = new PostAnteConverter(line);
			    if (postAnte.IsMatch())
			    {
				    ante = postAnte.ante;
				    break;
			    }
		    }
	    }
		public List<string> To888()
	    {
		    Initialize888();
		    if (SHOULD_CONVERT == false) return null; // return null - if not need to convert
		    var result = new List<string>();

		    var seatPreviewCount = 0;// or after seatPreview rows
		    var state = Enums.State.PreFlop;


		    foreach (var line in hand)
		    {
			    var uncalledBetConv = new UncalledBetConverter(line);
			    if (uncalledBetConv.IsMatch())//not using in 888
			    {
				    continue;
			    }

			    var collectedPotConv = new CollectedPotConverter(line, sidePotNumber, playersPutInAmount);
			    if (collectedPotConv.IsMatch())
			    {
				    result.Add(collectedPotConv.ConvertTo888());
				    sidePotNumber = collectedPotConv.sidePotNumber;
				    playersPutInAmount = collectedPotConv.playersPutInAmount;
				    continue;
			    }

			    #region FLOP TURN RIVER Converters and reset playersPutInAmount after each street
			    var flopConv = new FlopConverter(line);
			    if (flopConv.IsMatch())
			    {
				    state = Enums.State.Flop;
				    ResetPlayersPutInCollection();
				    result.Add(flopConv.ConvertTo888());
				    continue;
			    }

			    var turnConv = new TurnConverter(line);
			    if (turnConv.IsMatch())
			    {
				    state = Enums.State.Turn;
				    ResetPlayersPutInCollection();
				    result.Add(turnConv.ConvertTo888());
				    continue;
			    }

			    var riverConv = new RiverConverter(line);
			    if (riverConv.IsMatch())
			    {
				    state = Enums.State.River;
				    ResetPlayersPutInCollection();
				    result.Add(riverConv.ConvertTo888());
				    continue;
			    }
			    #endregion

			    var postBBConv = new PostBBConverter(line, playersPutInAmount);
			    if (postBBConv.IsMatch())
			    {
				    result.Add(postBBConv.ConvertTo888());
				    playersPutInAmount = postBBConv.playersPutInAmount;
				    continue;
			    }

			    var postSBConv = new PostSBConverter(line, playersPutInAmount);
			    if (postSBConv.IsMatch())
			    {
				    result.Add(postSBConv.ConvertTo888());
				    playersPutInAmount = postSBConv.playersPutInAmount;
				    continue;
			    }

			    var betConv = new BetConverter(line, playersPutInAmount);
			    if (betConv.IsMatch())
			    {
				    result.Add(betConv.ConvertTo888());
				    playersPutInAmount = betConv.playersPutInAmount;
				    continue;
			    }

			    var callConv = new CallConverter(line, playersPutInAmount);
			    if (callConv.IsMatch())
			    {
				    result.Add(callConv.ConvertTo888());
				    playersPutInAmount = callConv.playersPutInAmount;
				    continue;
			    }

			    var raiseConv = new RaiseConverter(line, playersPutInAmount);
			    if (raiseConv.IsMatch())
			    {
				    result.Add(raiseConv.ConvertTo888());
				    playersPutInAmount = raiseConv.playersPutInAmount;
				    continue;
			    }

			    var converters = new List<Converter>()
			    {
				    new CheckConverter(line),
				    new PostAnteConverter(line),
				    new HoleCardsConverter(line),
				    new DealtToPlayerConverter(line),
				    new PlayerShowsConverter(line),
				    new MuckHandConverter(line),
				    new HandHeaderConverter(line, ante),
				    new TableHeaderConverter(line, numberOfPlayers),
				    new FoldConverter(line),
					new SeatPreviewConverter(line)
				};

			    foreach (var converter in converters)
			    {
				    if (converter.IsMatch())
				    {
					    result.Add(converter.ConvertTo888());
					    break;
				    }
			    }
		    }
		    return result;
		}
		#endregion
	}
}
