<template>
  <div class="search-cards">
    <h1 class="my-cards-header">Community Flashcards</h1>
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
      class="card purple-btn btn btn-lg btn-primary btn-block "
      v-for="card in filteredCards"
      v-bind:key="card.cardId"
      v-bind:value="card.cardId"
      v-on:click="toggleFlipAnimate(card.cardId); toggleFlip(card.cardId); "
      v-on:mouseover="currentCard = card.cardId"
      v-on:mouseleave="currentCard = 0; showDropDown = false"
      
    >
      <!-- v-bind:class="{'flip-card-test': showBack.includes(card.cardId), 'flip-card-front-test': showFront.includes(card.cardId)}" -->
      <div class="hover-btn">
        <img
          class="edit card-button-color"
          src="../assets/add-outline.svg"
          v-show="currentCard == card.cardId"
          v-on:click.stop="showDropDown = true"
        />
        <select name="deck-names" id="deck-names" v-on:click.stop="" v-if="showDropDown && currentCard == card.cardId" v-model="deckSelection">
          <option value="none" selected disabled hidden>Choose</option>
          <option v-for="deck in decks" v-bind:key="deck.deckId" v-bind:value="deck.deckId">{{deck.deckName}}</option> 
        </select>
        <button class="add-card-btn" v-if="showDropDown && currentCard == card.cardId" v-on:click.stop="addCardToDeck()">Add</button>
      </div>

      <h3 class="list-cards-text" v-show="!showBack.includes(card.cardId)" v-on:click.self="toggleFlip(card.cardId)">{{ card.cardFront }}</h3>
      <img id="card-image-search" :src="(card.cardImage)" alt="card image" v-show="(!showBack.includes(card.cardId) && card.cardImage) ">
      <h3 class="list-cards-text text-back" v-show="showBack.includes(card.cardId)" v-on:click.self="toggleFlip(card.cardId)">{{ card.cardBack }}</h3>
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
      showFront: [],
      searchQuery: "",
      showDropDown: false,
      deckSelection: 0,
    };
  },
  methods: {
    toggleFlip(cardId) {
      if (!this.showBack.includes(cardId)) {
        setTimeout(() => {
        this.showBack.push(cardId);
            
          }, 200);
      } else {
        setTimeout(() => {
        this.showBack = this.showBack.filter((a) => {
          return a != cardId;
        });
            
          }, 200);
        
      }
    },
    toggleFlipAnimate(cardId){
      console.log(cardId);
      const cardClasses = document.getElementsByClassName("card");
      for(let i = 0; i < cardClasses.length; i++){
        if(cardClasses[i].value == cardId){
          cardClasses[i].classList.remove('flip-card-animate');
          setTimeout(() => {
          cardClasses[i].classList.add('flip-card-animate');
            
          }, 50);

        }
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
      .getPublicCards(this.$store.state.user.userId)
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
/* .text-back{
  transform: rotateX(-180deg);
} */
.flip-card-animate{
  animation-name: flip-card-animate;
  animation-duration: .3s;
  animation-fill-mode:forwards;
}
/* .flip-card-front-test{
  animation-name: flip-card-test-animate;
  animation-duration: 1s;
  animation-fill-mode:forwards;
} */
/* .search-card-flip-test{
  transition: transform 1s ease-in-out;
  transform: rotateX(90deg);
} */
@keyframes flip-card-animate{
    0% {transform: rotate3d(0);}
    50% {transform: rotate3d(1, .02, 0, 90deg);}
    100% {transform: rotate3d(0);}


}
#card-image-search {
    height: 10vh;
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

.card h3 {

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