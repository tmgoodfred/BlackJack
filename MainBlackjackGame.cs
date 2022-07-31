namespace BlackjackGame
{
    public partial class MainBlackjackGame : Form
    {
        int totalMoney = 100;    //starting money
        int betAmount = 0;
        int deckSize = 51;
        int userCardTotal = 0;
        int dealerCardTotal = 0;
        int userExtraCardAmount = 0;
        int dealerExtraCardAmount = 0;
        float userWins = 0f;
        float userGamesPlayed = 0f;
        string dealerSecondCard;
        Boolean hasBetBeenMade = false;
        Boolean aceBeenCheckedUser = false;
        Boolean aceBeenCheckedDealer = false;
        List<string> deck = new List<string> {"S2", "S3", "S4", "S5", "S6", "S7", "S8", "S9", "S10", "SJ", "SQ", "SK", "SA",
        "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10", "DJ", "DQ", "DK", "DA",
        "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "C10", "CJ", "CQ", "CK", "CA",
        "H2", "H3", "H4", "H5", "H6", "H7", "H8", "H9", "H10", "HJ", "HQ", "HK", "HA"};
        string[] backup = {
        "S2", "S3", "S4", "S5", "S6", "S7", "S8", "S9", "S10", "SJ", "SQ", "SK", "SA",
        "D2", "D3", "D4", "D5", "D6", "D7", "D8", "D9", "D10", "DJ", "DQ", "DK", "DA",
        "C2", "C3", "C4", "C5", "C6", "C7", "C8", "C9", "C10", "CJ", "CQ", "CK", "CA",
        "H2", "H3", "H4", "H5", "H6", "H7", "H8", "H9", "H10", "HJ", "HQ", "HK", "HA"};
        List<int> userCards = new List<int>();
        List<int> dealerCards = new List<int>();
        List<string> userHitCards = new List<string>();
        List<string> dealerHitCards = new List<string>();
        string cardBack = @"card_images\purple_back.png";

        Boolean gameOver = false;
        public MainBlackjackGame()
        {
            InitializeComponent();
            totalMoneyTextBox.Text = "$" + totalMoney.ToString();
            playerCard1.Image = Image.FromFile(@"card_images\purple_back.png");
            playerCard2.Image = Image.FromFile(@"card_images\purple_back.png");
            dealerCard1.Image = Image.FromFile(@"card_images\purple_back.png");
            dealerCard2.Image = Image.FromFile(@"card_images\purple_back.png");
            userExtraCard1.Visible = false;
            userExtraCard2.Visible = false;
            userExtraCard3.Visible = false;
            userExtraCard4.Visible = false;
            dealerCardExtra1.Visible = false;
            dealerCardExtra2.Visible = false;
            dealerCardExtra3.Visible = false;
            dealerCardExtra4.Visible = false;
            userExtraCard1.BackColor = Color.Transparent;
            userExtraCard2.BackColor = Color.Transparent;
            userExtraCard3.BackColor = Color.Transparent;
            userExtraCard4.BackColor = Color.Transparent;
            dealerCardExtra1.BackColor = Color.Transparent;
            dealerCardExtra2.BackColor = Color.Transparent;
            dealerCardExtra3.BackColor = Color.Transparent;
            dealerCardExtra4.BackColor = Color.Transparent;
            playerCard1.BackColor = Color.Transparent;
            dealerCard1.BackColor = Color.Transparent;
            playerCard2.BackColor = Color.Transparent;
            dealerCard2.BackColor = Color.Transparent;
        }

        private void dealButton_Click(object sender, EventArgs e)
        {
            gameOver = false;
            dealButton.Enabled = false;
            if (betAmount > 0)
            {
                hasBetBeenMade = true;
                //deal 2 cards to player, 2 to house, remove them from the deck list
                Random rnd = new Random();
                if (deck.Count < 4)  //shuffle function
                {
                    MessageBox.Show("Shuffling");
                    deck.Clear();
                    for (int i = 0; i < backup.Length; i++)
                    {
                        deck.Add(backup[i]);
                    }
                    deckSize = 51;  //reset deck size
                }
                for (int i = 0; i < 4; i++) //if you've got at least 4 cards in the deck, it'll deal 4 cards
                {
                    int card = rnd.Next(0, deckSize);
                    string dealtCard = deck[card];
                    deck.RemoveAt(card);
                    deckSize -= 1;

                    if (i == 0)
                    {
                        putImageInBox(dealtCard, "playerCard1");
                        userCardTotal += getCardAmount(dealtCard);
                        userCards.Add(getCardAmount(dealtCard));
                    }
                    else if (i == 1)
                    {
                        putImageInBox(dealtCard, "playerCard2");
                        userCardTotal += getCardAmount(dealtCard);
                        userCards.Add(getCardAmount(dealtCard));
                    }
                    else if (i == 2)
                    {
                        putImageInBox(dealtCard, "dealerCard1");
                        dealerCardTotal += getCardAmount(dealtCard);
                        dealerCards.Add(getCardAmount(dealtCard));
                    }
                    else if (i == 3)
                    {
                        dealerSecondCard = dealtCard;
                        dealerCardTotal += getCardAmount(dealtCard);
                        dealerCards.Add(getCardAmount(dealtCard));
                    }
                }
                userCardTotalBox.Text = userCardTotal.ToString();
                //dealerTotalBox.Text = dealerCardTotal.ToString();
            }
            else
            {
                MessageBox.Show("Place a bet first");
            }

            if(userCardTotal == 21) //if you get 21 on the deal, you don't want to hit, so I don't give the option, it just moves to the dealers turn.
            {
                MessageBox.Show("You got 21 on Deal!");
                standButton_Click(sender, e);
            }
        }
        private void hitButton_Click(object sender, EventArgs e)
        {
            if (hasBetBeenMade == true) //make sure the user has placed a bet before trying to hit
            {
                Random rnd = new Random();

                if (userCardTotal < 21) //we want to make sure that if the user has less than 21 they can hit
                {
                    if (deck.Count < 1)  //shuffle function
                    {
                        shuffleFunc();
                    }

                    int card = rnd.Next(0, deckSize);
                    string dealtCard = deck[card];
                    deck.RemoveAt(card);    //gets rid of card from deck so no dupes
                    deckSize -= 1;
                    userCardTotal += getCardAmount(dealtCard);
                    userCards.Add(getCardAmount(dealtCard));

                    userCardTotalBox.Text = userCardTotal.ToString();

                    userHitCards.Add(dealtCard);
                    string combinedString = string.Join(", ", userHitCards);
                    userExtraCardAmount += 1;
                    putImageInBox(dealtCard, $"userExtraCard{userExtraCardAmount}");

                    if (userCardTotal > 21) //if you've got over 21
                    {
                        if (userCards.Contains(11) && aceBeenCheckedUser != true) //but if you've got an ACE, it will automatically change it from an 11 to a 1.
                        {
                            userCardTotal -= 10;
                            userCardTotalBox.Text = userCardTotal.ToString();   //update the total
                            aceBeenCheckedUser = true;
                        }
                        else //if there's no ACE and you have over 21, you lose.
                        {
                            gameResult("playerBusts");
                        }
                    }
                    if (userCardTotal == 21)    //if you get 21 after another hit, it will stop and move you on to the dealers turn
                    {
                        //do something to continue it to dealer stuff
                        MessageBox.Show("You got 21!");
                        standButton_Click(sender, e);
                    }
                }
            }
        }
        private void standButton_Click(object sender, EventArgs e)  //could also be referred to as the dealer's turn
        {
            //when stand is pressed, remove the masking on dealer card 2, and reveal the dealer total card amount
            putImageInBox(dealerSecondCard, "dealerCard2");
            hitButton.Enabled = false;   //don't allow user to hit one they've stood
            dealerTotalBox.Text = dealerCardTotal.ToString();   //we now show the user the dealer's card values
            if (hasBetBeenMade == true && gameOver != true)
            {
                Random rnd = new Random();
                if (dealerCardTotal > userCardTotal)    //if you hit stand and the dealer starts out with a higher value than you, you lose
                {
                    gameResult("dealerWins");
                }
                else if (dealerCardTotal == userCardTotal && dealerCardTotal <= 21) //if user and dealer have same value, break even
                {
                    gameResult("breakEven");
                }
                else if (dealerCardTotal == 17 && userCardTotal > dealerCardTotal && gameOver != true)  //if the dealer has exactly 17 and you have higher, you win
                {
                    gameResult("playerWins");
                    gameOver = true;
                }
                else if (userCardTotal > dealerCardTotal && dealerCardTotal >= 17 && dealerCardTotal < 21)   //if dealer has between 17 and 20 and the user has higher, than user wins
                {
                    gameResult("playerWins");
                    gameOver = true;
                }
                else
                {
                    while (dealerCardTotal < 17 && gameOver != true)
                    {
                        if (deck.Count < 1)  //shuffle function
                        {
                            shuffleFunc();
                        }

                        int card = rnd.Next(0, deckSize);
                        string dealtCard = deck[card];
                        deck.RemoveAt(card);    //gets rid of card from deck so no dupes
                        deckSize -= 1;
                        dealerCardTotal += getCardAmount(dealtCard);
                        dealerCards.Add(getCardAmount(dealtCard));

                        dealerTotalBox.Text = dealerCardTotal.ToString();

                        dealerHitCards.Add(dealtCard);
                        string combinedString = string.Join(", ", dealerHitCards);
                        dealerExtraCardAmount += 1;
                        putImageInBox(dealtCard, $"dealerCardExtra{dealerExtraCardAmount}");

                        if (dealerCardTotal > 21)   //if dealer busts, player wins
                        {
                            if (dealerCards.Contains(11) && aceBeenCheckedDealer != true)   //if dealer has an ace and is about to bust, change from 11 to 1
                            {
                                dealerCardTotal -= 10;
                                dealerTotalBox.Text = dealerCardTotal.ToString();   //update the total
                                aceBeenCheckedDealer = true;
                                standButton_Click(sender, e);
                            }
                            else
                            {
                                gameResult("playerWins");
                                break;
                            }
                        }
                        else if (dealerCardTotal > userCardTotal && dealerCardTotal <= 21 && gameOver != true)    //if dealer has greater cards than player, player loses
                        {
                            gameResult("dealerWins");
                            break;
                        }
                        else if (dealerCardTotal == userCardTotal && gameOver != true)  //if dealer and user have same cards
                        {
                            gameResult("breakEven");
                            break;
                        }
                        else if (dealerCardTotal == 17)  //if dealer hits a 17, he has to stop and we'll check to see if we win
                        {
                            if (userCardTotal > dealerCardTotal)
                            {
                                gameResult("playerWins");
                                break;
                            }
                            else
                                gameResult("dealerWins");
                        }
                        else if (userCardTotal > dealerCardTotal && dealerCardTotal >= 17)
                        {
                            gameResult("playerWins");
                            break;
                        }
                    }
                }
            }
        }
        private int getCardAmount(string rawData)
        {
            int cardTotal = 0;
            var brokenString = rawData.ToCharArray();

            if (brokenString[1].ToString().Equals("J"))
            {
                cardTotal = 10;
            }
            else if (brokenString[1].ToString().Equals("Q"))
            {
                cardTotal = 10;
            }
            else if (brokenString[1].ToString().Equals("K"))
            {
                cardTotal = 10;
            }
            else if (brokenString[1].ToString().Equals("A"))    //automatically sets ACE to 11, and in other areas if you go over 21 but have an ACE it will change its value to 1
            {
                cardTotal = 11;
            }
            else if (brokenString.Length == 3)  //if it's a 10 card like A10 or C10, which had 3 char
            {
                cardTotal = 10;
            }
            else
            {
                cardTotal = Int32.Parse(brokenString[1].ToString());
            }

            return cardTotal;
        }

        private void oneDPlusButton_Click(object sender, EventArgs e)
        {
            if (totalMoney > 0)
            {
                betAmount += 1;
                betAmountTextBox.Text = "$" + betAmount.ToString();
                totalMoney -= 1;
                totalMoneyTextBox.Text = "$" + totalMoney.ToString();
            }
            else
            {
                MessageBox.Show("Uh oh! Out of money!");
            }
        }
        private void oneDMinusButton_Click(object sender, EventArgs e)
        {
            if (betAmount > 0)
            {
                betAmount -= 1;
                betAmountTextBox.Text = "$" + betAmount.ToString();
                totalMoney += 1;
                totalMoneyTextBox.Text = "$" + totalMoney.ToString();
            }
            else
            {
                MessageBox.Show("Uh oh! Not enough to remove!");
            }
        }
        private void fiveDMinusButton_Click(object sender, EventArgs e)
        {
            if (betAmount >= 5)
            {
                betAmount -= 5;
                betAmountTextBox.Text = "$" + betAmount.ToString();
                totalMoney += 5;
                totalMoneyTextBox.Text = "$" + totalMoney.ToString();
            }
            else
            {
                MessageBox.Show("Uh oh! Not enough to remove!");
            }
        }
        private void fiveDPlusButton_Click(object sender, EventArgs e)
        {
            if (totalMoney >= 5)
            {
                betAmount += 5;
                betAmountTextBox.Text = "$" + betAmount.ToString();
                totalMoney -= 5;
                totalMoneyTextBox.Text = "$" + totalMoney.ToString();
            }
            else
            {
                MessageBox.Show("Uh oh! Try not being broke!");
            }
        }
        private void twentyfiveDMinusButton_Click(object sender, EventArgs e)
        {
            if (betAmount > 25)
            {
                betAmount -= 25;
                betAmountTextBox.Text = "$" + betAmount.ToString();
                totalMoney += 25;
                totalMoneyTextBox.Text = "$" + totalMoney.ToString();
            }
            else
            {
                MessageBox.Show("Uh oh! Not enough to remove!");
            }
        }
        private void twentyfiveDPlusButton_Click(object sender, EventArgs e)
        {
            if (totalMoney >= 25)
            {
                betAmount += 25;
                betAmountTextBox.Text = "$" + betAmount.ToString();
                totalMoney -= 25;
                totalMoneyTextBox.Text = "$" + totalMoney.ToString();
            }
            else
            {
                MessageBox.Show("Uh oh! Try not being broke!");
            }
        }
        private void onehundredDMinusButton_Click(object sender, EventArgs e)
        {
            if (betAmount > 100)
            {
                betAmount -= 100;
                betAmountTextBox.Text = "$" + betAmount.ToString();
                totalMoney += 100;
                totalMoneyTextBox.Text = "$" + totalMoney.ToString();
            }
            else
            {
                MessageBox.Show("Uh oh! Not enough to remove!");
            }
        }
        private void onehundredDPlusButton_Click(object sender, EventArgs e)
        {
            if (totalMoney >= 100)
            {
                betAmount += 100;
                betAmountTextBox.Text = "$" + betAmount.ToString();
                totalMoney -= 100;
                totalMoneyTextBox.Text = "$" + totalMoney.ToString();
            }
            else
            {
                MessageBox.Show("Uh oh! Try not being broke!");
            }
        }
        
        private void shuffleFunc()
        {
            MessageBox.Show("Shuffling");
            deck.Clear();
            for (int i = 0; i < backup.Length; i++)
            {
                deck.Add(backup[i]);
            }
            deckSize = 51;  //reset deck size
        }

        private void gameResult(string decision)
        {
            if (decision.Equals("playerWins"))
            {
                MessageBox.Show("You Win!");
                totalMoney += betAmount * 2;
                totalMoneyTextBox.Text = "$" + totalMoney.ToString();
                userWins += 1f;
                userGamesPlayed += 1f;
            }
            else if (decision.Equals("dealerWins"))
            {
                MessageBox.Show("You lose, dealer higher cards");
                userGamesPlayed += 1f;
            }
            else if (decision.Equals("playerBusts"))
            {
                MessageBox.Show("You lose, over 21");
                userGamesPlayed += 1f;
            }
            else if (decision.Equals("breakEven"))
            {
                MessageBox.Show("Break Even, same cards");  //it seems to go here 
                totalMoney += betAmount;
                totalMoneyTextBox.Text = "$" + totalMoney.ToString();
                //no user games played add because we don't want to throw off any stats
            }

            calcWinrate(userWins, userGamesPlayed);
            betAmount = 0;
            betAmountTextBox.Text = "$0";
            userCardTotalBox.Clear();
            userCardTotal = 0;
            dealerCardTotal = 0;
            dealerCards.Clear();
            userCards.Clear();
            userHitCards.Clear();
            dealerHitCards.Clear();
            dealerTotalBox.Clear();
            aceBeenCheckedUser = false;
            aceBeenCheckedDealer = false;
            userExtraCardAmount = 0;
            dealerExtraCardAmount = 0;
            userExtraCard1.Visible = false;
            userExtraCard2.Visible = false;
            userExtraCard3.Visible = false;
            userExtraCard4.Visible = false;
            dealerCardExtra1.Visible = false;
            dealerCardExtra2.Visible = false;
            dealerCardExtra3.Visible = false;
            dealerCardExtra4.Visible = false;
            playerCard1.Image = Image.FromFile(cardBack);
            playerCard2.Image = Image.FromFile(cardBack);
            dealerCard1.Image = Image.FromFile(cardBack);
            dealerCard2.Image = Image.FromFile(cardBack);
            hitButton.Enabled = true;   //re-enable after you finish the game
            dealButton.Enabled = true;
            hasBetBeenMade = false;
            gameOver = true;
        }

        private void calcWinrate(float wins, float games)
        {
            winrateBox.Clear();
            float winrate = (wins / games) * 100;
            winrateBox.Text = winrate.ToString("0.00") + "%";
        }

        private void putImageInBox(string card, string slot)
        {
            string reversed;
            for (int i = 0; i < backup.Length; i++)
            {
                if (backup[i] == card)
                {

                    var brokenString = card.ToCharArray();
                    if (brokenString.Length != 3)   
                    {
                        reversed = brokenString[1].ToString()+brokenString[0].ToString();
                    }
                    else //if it's a 10 card, the numbers need to be slightly rearranged
                    {
                        reversed = brokenString[1].ToString() + brokenString[2].ToString() + brokenString[0].ToString();
                    }

                    if (slot.Equals("playerCard1"))
                    {
                        playerCard1.Image = Image.FromFile($@"card_images\{reversed}.png");
                    }
                    else if (slot.Equals("playerCard2"))
                    {
                        playerCard2.Image = Image.FromFile($@"card_images\{reversed}.png");
                    }
                    else if (slot.Equals("dealerCard1"))
                    {
                        dealerCard1.Image = Image.FromFile($@"card_images\{reversed}.png");
                    }
                    else if (slot.Equals("dealerCard2"))
                    {
                        dealerCard2.Image = Image.FromFile($@"card_images\{reversed}.png");
                    }
                    else if (slot.Equals("userExtraCard1"))
                    {
                        userExtraCard1.Visible = true;
                        userExtraCard1.Image = Image.FromFile($@"card_images\{reversed}.png");
                    }
                    else if (slot.Equals("userExtraCard2"))
                    {
                        userExtraCard2.Visible = true;
                        userExtraCard2.Image = Image.FromFile($@"card_images\{reversed}.png");
                    }
                    else if (slot.Equals("userExtraCard3"))
                    {
                        userExtraCard3.Visible = true;
                        userExtraCard3.Image = Image.FromFile($@"card_images\{reversed}.png");
                    }
                    else if (slot.Equals("userExtraCard4"))
                    {
                        userExtraCard4.Visible = true;
                        userExtraCard4.Image = Image.FromFile($@"card_images\{reversed}.png");
                    }
                    else if (slot.Equals("dealerCardExtra1"))
                    {
                        dealerCardExtra1.Visible = true;
                        dealerCardExtra1.Image = Image.FromFile($@"card_images\{reversed}.png");
                    }
                    else if (slot.Equals("dealerCardExtra2"))
                    {
                        dealerCardExtra2.Visible = true;
                        dealerCardExtra2.Image = Image.FromFile($@"card_images\{reversed}.png");
                    }
                    else if (slot.Equals("dealerCardExtra3"))
                    {
                        dealerCardExtra3.Visible = true;
                        dealerCardExtra3.Image = Image.FromFile($@"card_images\{reversed}.png");
                    }
                    else if (slot.Equals("dealerCardExtra4"))
                    {
                        dealerCardExtra4.Visible = true;
                        dealerCardExtra4.Image = Image.FromFile($@"card_images\{reversed}.png");
                    }
                }
            }
        }

        private void helpButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("How to play?\n" +
                "The player wants to get as close to or hit a total of 21\n" +
                "and the Dealer will want to get a higher score than the player.\n" +
                "You want to hit until you get close to 21, but if you go over you bust and lose!\n" +
                "The dealer will hit until they get at least 17");
        }
    }
}