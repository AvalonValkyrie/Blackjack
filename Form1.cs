using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Baldwin_Asg1_Blackjack
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //Def Area Start
        //General Def Area Start
        public int totalPlayerWins = 0;
        public int totalDealerWins = 0;
        public int totalTies = 0;
        public int totalGamesPlayed = 0;
        public string winnerMessage = "";
        enum Suits { Clubs, Daimonds, Hearts, Spades };
        struct PlayingCard
        {
            public Suits suit;
            public int rank;
            public int imageListIndex;
            public bool isFaceVisible;
            public int bjValue;
        }
        List<PlayingCard> listDeck = new List<PlayingCard>();
        List<PlayingCard> listCardsPlayer = new List<PlayingCard>();
        List<PlayingCard> listCardsDealer = new List<PlayingCard>();
        List<PictureBox> listPicBoxDealersCards = new List<PictureBox>();
        List<PictureBox> listPicBoxPlayersCards = new List<PictureBox>();
        PictureBox[] playerPictureBoxArray = new PictureBox[8];
        PictureBox[] dealerPictureBoxArray = new PictureBox[8];
        //General Def Area End
        //Method Def Area Start
        private void Form1_Load(object sender, EventArgs e)
        {
            playerPictureBoxArray[0] = pictureBoxPlayer1;
            playerPictureBoxArray[1] = pictureBoxPlayer2;
            playerPictureBoxArray[2] = pictureBoxPlayer3;
            playerPictureBoxArray[3] = pictureBoxPlayer4;
            playerPictureBoxArray[4] = pictureBoxPlayer5;
            playerPictureBoxArray[5] = pictureBoxPlayer6;
            playerPictureBoxArray[6] = pictureBoxPlayer7;
            playerPictureBoxArray[7] = pictureBoxPlayer8;

            dealerPictureBoxArray[0] = pictureBoxDealer1;
            dealerPictureBoxArray[1] = pictureBoxDealer2;
            dealerPictureBoxArray[2] = pictureBoxDealer3;
            dealerPictureBoxArray[3] = pictureBoxDealer4;
            dealerPictureBoxArray[4] = pictureBoxDealer5;
            dealerPictureBoxArray[5] = pictureBoxDealer6;
            dealerPictureBoxArray[6] = pictureBoxDealer7;
            dealerPictureBoxArray[7] = pictureBoxDealer8;

            labelDealerTotal.Text = "0";
            labelPlayerTotal.Text = "0";
            resetHands();

            labelDealerWins.Text = totalDealerWins.ToString();
            labelPlayerWins.Text = totalPlayerWins.ToString();
            labelTotalGames.Text = totalGamesPlayed.ToString();
            labelTotalTies.Text = totalTies.ToString();


            dealCards();

            loadDeckManually();
            loadPictureBoxesIntoLists();
            hidePictureBoxes();
        }
        private int getPlayerTotal()
        {
            int aceCount = 0;
            int total = 0;
            for (int i = 0; i < listCardsPlayer.Count; i++)
            {
                if (listCardsPlayer[i].rank == 1)
                {
                    aceCount++;
                }

                total = total + listCardsPlayer[i].bjValue;
            }
            if (aceCount > 0)
            {
                if (total >= 7 && total <= 11)
                {
                    total = total + 10;
                }
            }
            return total;
        }
        private int getDealerTotal()
        {
            int aceCount = 0;
            int total = 0;
            for (int i = 0; i < listCardsDealer.Count; i++)
            {
                if (listCardsDealer[i].rank == 1)
                {
                    aceCount++;
                }

                total = total + listCardsDealer[i].bjValue;
            }
            if (aceCount > 0)
            {
                if (total >= 7 && total <= 11)
                {
                    total = total + 10;
                }
            }
            return total;
        }
        private void loadPictureBoxesIntoLists()
        {
            listPicBoxDealersCards.Add(pictureBoxDealer1);
            listPicBoxDealersCards.Add(pictureBoxDealer2);
            listPicBoxDealersCards.Add(pictureBoxDealer3);
            listPicBoxDealersCards.Add(pictureBoxDealer4);
            listPicBoxDealersCards.Add(pictureBoxDealer5);
            listPicBoxDealersCards.Add(pictureBoxDealer6);
            listPicBoxDealersCards.Add(pictureBoxDealer7);
            listPicBoxDealersCards.Add(pictureBoxDealer8);

            listPicBoxPlayersCards.Add(pictureBoxPlayer1);
            listPicBoxPlayersCards.Add(pictureBoxPlayer2);
            listPicBoxPlayersCards.Add(pictureBoxPlayer3);
            listPicBoxPlayersCards.Add(pictureBoxPlayer4);
            listPicBoxPlayersCards.Add(pictureBoxPlayer5);
            listPicBoxPlayersCards.Add(pictureBoxPlayer6);
            listPicBoxPlayersCards.Add(pictureBoxPlayer7);
            listPicBoxPlayersCards.Add(pictureBoxPlayer8);
        }
        private void hidePictureBoxes()
        {
            for (int i = 0; i < 8; i++)
            {
                listPicBoxDealersCards[i].Visible = false;
                listPicBoxPlayersCards[i].Visible = false;
            }
        }
        private void resetHands()
        {
            labelPlayerTotal.Text = "0";
            labelDealerTotal.Text = "0";
            labelGameResult.Text = "";
            foreach (PictureBox picBox in playerPictureBoxArray)
            {
                picBox.Visible = false;
            }
            foreach (PictureBox picBox in dealerPictureBoxArray)
            {
                picBox.Visible = false;
            }
        }

        private void dealCards()
        {
            listCardsPlayer.Clear();
            listCardsDealer.Clear();
            loadDeckManually();
            resetHands();
            buttonHit.Enabled = true;

            PlayingCard card = new PlayingCard();
            card = pullRandomCardFromDeck();
            listCardsPlayer.Add(card);
            pictureBoxPlayer1.Visible = true;
            pictureBoxPlayer1.Image = imgListCards.Images[card.imageListIndex];
            card = pullRandomCardFromDeck();
            listCardsPlayer.Add(card);
            pictureBoxPlayer2.Visible = true;
            pictureBoxPlayer2.Image = imgListCards.Images[card.imageListIndex];

            card = pullRandomCardFromDeck();
            listCardsDealer.Add(card);
            pictureBoxDealer1.Visible = true;
            pictureBoxDealer1.Image = imgListCards.Images[card.imageListIndex];
            card = pullRandomCardFromDeck();
            listCardsDealer.Add(card);
            labelDealerTotal.Text = getDealerTotal().ToString();
            labelPlayerTotal.Text = getPlayerTotal().ToString();
        }
        private void displayNextDealerCard()
        {
            int cardNumber = listCardsDealer.Count;
            PlayingCard dealerCard = listCardsDealer[cardNumber - 1];

            if (cardNumber == 3)
            {
                pictureBoxDealer3.Visible = true;
                pictureBoxDealer3.Image = imgListCards.Images[dealerCard.imageListIndex];
            }
            else if (cardNumber == 4)
            {
                pictureBoxDealer4.Visible = true;
                pictureBoxDealer4.Image = imgListCards.Images[dealerCard.imageListIndex];
            }
            else if (cardNumber == 5)
            {
                pictureBoxDealer5.Visible = true;
                pictureBoxDealer5.Image = imgListCards.Images[dealerCard.imageListIndex];
            }
            else if (cardNumber == 6)
            {
                pictureBoxDealer6.Visible = true;
                pictureBoxDealer6.Image = imgListCards.Images[dealerCard.imageListIndex];
            }
            else if (cardNumber == 7)
            {
                pictureBoxDealer7.Visible = true;
                pictureBoxDealer7.Image = imgListCards.Images[dealerCard.imageListIndex];
            }
            else if (cardNumber == 8)
            {
                pictureBoxDealer8.Visible = true;
                pictureBoxDealer8.Image = imgListCards.Images[dealerCard.imageListIndex];
            }
        }
        private void playDealerHand()
        {
            int dealerTotal = getDealerTotal();
            pictureBoxDealer2.Visible = true;
            pictureBoxDealer2.Image = imgListCards.Images[listCardsDealer[1].imageListIndex];

            if (getPlayerTotal() < 22)
            {
                while (dealerTotal < 17)
                {
                    PlayingCard card = pullRandomCardFromDeck();
                    listCardsDealer.Add(card);

                    displayNextDealerCard();
                    dealerTotal = getDealerTotal();
                }
                checkIfWinner();
            }
            labelDealerTotal.Text = getDealerTotal().ToString();

            buttonDealCards.Enabled = true;
        }

        private void loadDeckManually()
        {
            listDeck.Clear();
            int rank = 1;
            int imageListIndex = 0;
            int bjValue = 1;
            bool isFaceVisible = true;

            PlayingCard card = new PlayingCard();

            for (int i = 1; i < 14; i++)
            {
                card.rank = rank;
                card.suit = Suits.Clubs;
                card.imageListIndex = imageListIndex;
                card.bjValue = bjValue;
                card.isFaceVisible = isFaceVisible;
                listDeck.Add(card);
                imageListIndex++;

                card.rank = rank;
                card.suit = Suits.Daimonds;
                card.imageListIndex = imageListIndex;
                card.bjValue = bjValue;
                card.isFaceVisible = isFaceVisible;
                listDeck.Add(card);
                imageListIndex++;

                card.rank = rank;
                card.suit = Suits.Hearts;
                card.imageListIndex = imageListIndex;
                card.bjValue = bjValue;
                card.isFaceVisible = isFaceVisible;
                listDeck.Add(card);
                imageListIndex++;

                card.rank = rank;
                card.suit = Suits.Spades;
                card.imageListIndex = imageListIndex;
                card.bjValue = bjValue;
                card.isFaceVisible = isFaceVisible;
                listDeck.Add(card);
                imageListIndex++;

                rank++;
                bjValue++;
                if (bjValue > 10)
                {
                    bjValue = 10;
                }
            }
        }
        private PlayingCard pullRandomCardFromDeck()
        {
            PlayingCard card;
            int cardCount = listDeck.Count;

            Random random = new Random(Guid.NewGuid().GetHashCode());
            int randomNumber = random.Next(0, cardCount);

            card = (PlayingCard)listDeck[randomNumber];
            listDeck.RemoveAt(randomNumber);

            return card;
        }
        private void displayPlayerCard()
        {
            int cardNumber = listCardsPlayer.Count;
            PlayingCard playerCard = listCardsPlayer[cardNumber - 1];

            if (cardNumber == 3)
            {
                pictureBoxPlayer3.Visible = true;
                pictureBoxPlayer3.Image = imgListCards.Images[playerCard.imageListIndex];
            }
            else if (cardNumber == 4)
            {
                pictureBoxPlayer4.Visible = true;
                pictureBoxPlayer4.Image = imgListCards.Images[playerCard.imageListIndex];
            }
            else if (cardNumber == 5)
            {
                pictureBoxPlayer5.Visible = true;
                pictureBoxPlayer5.Image = imgListCards.Images[playerCard.imageListIndex];
            }
            else if (cardNumber == 6)
            {
                pictureBoxPlayer6.Visible = true;
                pictureBoxPlayer6.Image = imgListCards.Images[playerCard.imageListIndex];
            }
            else if (cardNumber == 7)
            {
                pictureBoxPlayer7.Visible = true;
                pictureBoxPlayer7.Image = imgListCards.Images[playerCard.imageListIndex];
            }
            else if (cardNumber == 8)
            {
                pictureBoxPlayer8.Visible = true;
                pictureBoxPlayer8.Image = imgListCards.Images[playerCard.imageListIndex];
            }

        }
        private void showPlayerScore()
        {
            int playerTotal = getPlayerTotal();
            labelPlayerTotal.Text = playerTotal.ToString();
            if (playerTotal > 21)
            {
                labelGameResult.Text = "Player has been busted!";
                buttonHit.Enabled = false;
                buttonStand.Enabled = false;
                playDealerHand();
            }
        }
        private void playerHit()
        {
            PlayingCard card = pullRandomCardFromDeck();
            listCardsPlayer.Add(card);
            displayPlayerCard();
            showPlayerScore();
        }
        private void checkIfWinner()
        {
            int playerScore = getPlayerTotal();
            int dealerScore = getDealerTotal();
            if (playerScore > 21)
            {
                totalDealerWins += 1;
                labelDealerTotal.Text = getDealerTotal().ToString();
                labelGameResult.Text = "Player has been busted!";
                totalGamesPlayed += 1;
                labelTotalGames.Text = totalGamesPlayed.ToString();
            }
            else if (dealerScore > 21)
            {
                totalPlayerWins += 1;
                labelPlayerTotal.Text = getPlayerTotal().ToString();
                labelGameResult.Text = "Dealer has been busted!";
                totalGamesPlayed += 1;
                labelTotalGames.Text = totalGamesPlayed.ToString();
            }
            else if (buttonStand.Enabled == false)
            {
                if (dealerScore > playerScore)
                {
                    totalDealerWins += 1;
                    labelDealerTotal.Text = getDealerTotal().ToString();
                    labelGameResult.Text = "Dealer wins!";
                    totalGamesPlayed += 1;
                    labelTotalGames.Text = totalGamesPlayed.ToString();
                }
                else if (dealerScore < playerScore)
                {
                    totalPlayerWins += 1;
                    labelPlayerTotal.Text = getPlayerTotal().ToString();
                    labelGameResult.Text = "Player wins!";
                    totalGamesPlayed += 1;
                    labelTotalGames.Text = totalGamesPlayed.ToString();
                }
                else if (dealerScore == playerScore)
                {
                    totalTies += 1;
                    labelTotalTies.Text = totalTies.ToString();
                    labelGameResult.Text = "It's a tie";
                    totalGamesPlayed += 1;
                    labelTotalGames.Text = totalGamesPlayed.ToString();
                }
            }
        }
        //Method Def Area End
        //Def Area End

        //Button Area Start
        private void buttonDealCards_Click(object sender, EventArgs e)
        {
            resetHands();
            dealCards();
            buttonDealCards.Enabled = false;
            buttonHit.Enabled = true;
            buttonStand.Enabled = true;
        }

        private void buttonHit_Click(object sender, EventArgs e)
        {
            playerHit();
            checkIfWinner();
        }

        private void buttonStand_Click(object sender, EventArgs e)
        {
            buttonHit.Enabled = false;
            buttonStand.Enabled = false;
            playDealerHand();
            checkIfWinner();
        }
        //Button Area End
    }
}
