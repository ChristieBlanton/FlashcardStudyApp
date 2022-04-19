<template>
  <div class="list-cards">
    <button class="card purple-btn btn btn-lg btn-primary btn-block" 
        v-for="card in cards"  
        v-bind:key="card.cardId" 
        v-on:click.self="toggleFlip(card.cardId)"
        v-on:mouseover="currentCard = card.cardId" 
        v-on:mouseleave="currentCard = 0" >
        <div class="hover-btn">
        <img class="edit" src="../assets/cog.svg" v-show="currentCard == card.cardId" v-on:click="editCard(card)" />
        <img class="edit" src="../assets/close.svg" v-show="currentCard == card.cardId" v-on:click="deleteCard(currentCard)" />

      </div>
      <h3 class="list-cards-text" v-if="!showBack.includes(card.cardId)">{{ card.cardFront }}</h3>
      <h4 class="list-cards-text" v-else>{{ card.cardBack }}</h4>
    </button>
  </div>
</template>

<script>
import cardService from "../services/CardService";
export default {
  name: "list-cards",
  data() {
    return {
      showBack: [],
      currentCard: 0,
    };
  },
  methods: {
      toggleFlip(cardId){
          if(!this.showBack.includes(cardId)){
              this.showBack.push(cardId);
          }
          else{
              this.showBack = this.showBack.filter(a => {
                  return a != cardId;
              })
          }
      },
      deleteCard(cardId){
        cardService.deleteCard(this.$route.params.deckId, cardId).then(() => {
        cardService.getCardsInDeck(parseInt(this.$route.params.deckId)).then((response) => {this.$store.commit('SET_CARDS',response.data)})
      });
      },
      editCard(card){
        this.$store.commit("EDIT_CARD", card)
        this.$router.push({name: "editcard", params: {deckId: this.$route.params.deckId, cardId: this.currentCard}})
      }
  },
  created() {

    cardService.getCardsInDeck(parseInt(this.$route.params.deckId)).then((response) => {this.$store.commit("SET_CARDS", response.data)});
  },
  computed: {
    cards(){
      return this.$store.state.cards;
    }
  }
};
</script>

<style>
.card {
  width: 18vw;
  height: 20vh;
  padding: 10px;
  display: flex;
  cursor: default !important;
  overflow: scroll;

  border-width: 3px;
  border-color: rgba(255, 255, 255, 0.301);
  border-style: solid;
}
.card:hover{
  border-color: rgb(56, 255, 255) !important;
}
.card:focus{
  background-color: #593cda;
  border-color: rgba(255, 255, 255, 0.301);
  
  box-shadow: 0 3px 5px gray;
}

.card::-webkit-scrollbar {
  width: 20px;
}
.card::-webkit-scrollbar-corner {
  opacity: 0;
}
.list-cards{
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
  gap: 10px;
  margin-top: 40px;
}
a:hover{
  text-decoration: none;

}
.list-cards-text{
  pointer-events: none;
}
.hover-btn{
  height: 40px;
  width: 100%;
  display: flex;
  justify-content: space-between;
}
.edit{
  width: 35px;
  cursor: pointer;
}
</style>