<template>
  <div class="search-cards">
    <h1 class="my-cards-header">My Flashcards</h1>
    <div class="search-form">
      <input
        class="search-cards-input"
        type="text"
        name="search-query"
        id="search-query"
        placeholder="Search Cards By Tag"
        v-model="searchQuery"
      />
    </div>
    <div class="search-cards-container">
    <button
      class="card purple-btn btn btn-lg btn-primary btn-block"
      v-for="card in filteredCards"
      v-bind:key="card.cardId"
      v-on:click.self="toggleFlip(card.cardId)"
      v-on:mouseover="currentCard = card.cardId"
      v-on:mouseleave="currentCard = 0; showDropDown = false"
    >
      <div class="hover-btn">
        <img
          class="edit card-button-color"
          src="../assets/add-outline.svg"
          v-show="currentCard == card.cardId"
          v-on:click="showDropDown = true"
        />
        <select name="deck-names" id="deck-names" v-if="showDropDown && currentCard == card.cardId" v-model="deckSelection">
          <option value="none" selected disabled hidden>Choose</option>
          <option v-for="deck in decks" v-bind:key="deck.deckId" v-bind:value="deck.deckId">{{deck.deckName}}</option> 
        </select>
        <button class="add-card-btn" v-if="showDropDown && currentCard == card.cardId" v-on:click="addCardToDeck()">Add</button>
      </div>
      <h3 class="list-cards-text" v-if="!showBack.includes(card.cardId)" v-on:click.self="toggleFlip(card.cardId)">{{ card.cardFront }}</h3>
      <h3 class="list-cards-text" v-else v-on:click.self="toggleFlip(card.cardId)">{{ card.cardBack }}</h3>
      <div class="card-tags">
        <p class="tag-name" v-for="tag in card.tags" v-bind:key="tag">
          {{ tag }}
        </p>
      </div>
    </button>
    </div>
  </div>
</template>

<script>
import cardService from "../services/CardService";
import deckService from "../services/DeckService";
export default {
  name: "search-cards",
  data() {
    return {
      cards: [],
      decks: [],
      deckCards: [
        {deckId: 0,
         cardIds: []}
      ],
      currentCard: 0,
      showBack: [],
      searchQuery: "",
      showDropDown: false,
      deckSelection: 0,
    };
  },
  methods: {
    toggleFlip(cardId) {
      if (!this.showBack.includes(cardId)) {
        this.showBack.push(cardId);
      } else {
        this.showBack = this.showBack.filter((a) => {
          return a != cardId;
        });
      }
    },
    addCardToDeck() {
      cardService.getCardsInDeck(this.deckSelection).then(response => {
        if(!response.data.filter(card => {
          return card.cardId == this.currentCard
        }).length > 0) {
          cardService.addCardToDeck(this.currentCard, this.deckSelection);
          }
      })
      this.showDropDown = false;
    },
    isCardInDeck(deckId) {
      cardService.getCardsInDeck(deckId).then(response => {
        if(!response.data.filter(card => {
          return card.cardId == this.currentCard
        }).length > 0) {
          return true;
          }
      })
      return false;
    },
    // filteredDecks() {
    //   return this.decks.filter(deck => {
    //     return this.deckCards.filter(d => {
    //       return !d.cardIds.includes(this.currentCard)
    //     }).deckId == deck.deckId 
    //   })
    // }
  },
  created() {
    cardService
      .getCardsByUser(this.$store.state.user.userId)
      .then((response) => {
        this.cards = response.data;
      });
    deckService.myDecks(this.$store.state.user.userId).then((response) => {
      this.decks = response.data
      // for(let i = 0; i < this.decks.length; i++) {
      //   cardService.getCardsInDeck(this.decks[i].deckId).then(response => {
      //     this.deckCards.push({deckId: this.deck[i].deckId, cardIds: response.data.map(card => card.cardId)})
      //   })
      // }
      });
  },
  computed: {
    filteredCards() {
      if (this.searchQuery) {
        return this.cards.filter((card) => {
          return (
            card.tags.filter((tag) => {
              return tag.startsWith(this.searchQuery);
            }).length > 0
          );
        });
      } else {
        return this.cards;
      }
    },
    
  },
};
</script>

<style>
.search-cards-input{
  width: 15vw;
  margin-bottom: 5vh;
}
.my-cards-header{
  margin: 0 0 50px 0;
  flex-grow: 1;
}
.add-card-btn{
  box-shadow: none;
  padding: none;
  margin: none;
  font-size: 15px;
  background-color: #cacaca;
  align-items: center;
  border-radius: 5px;
}
.card-txt{
  flex-grow: 1;
}
.card-tags {
  display: flex;
  gap: 5px;
}
.card {
  width: 350px;
  height: 200px;
  padding: 10px;
  display: flex;
  justify-content: flex-start;
}
.search-cards {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 10px;
  margin-top: 40px;
}
.search-cards-container{
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
}
a:hover {
  text-decoration: none;
}
.hover-btn {
  height: 40px;
  width: 100%;
  display: flex;
  justify-content: space-between;
}
.edit {
  width: 35px;
}
</style>