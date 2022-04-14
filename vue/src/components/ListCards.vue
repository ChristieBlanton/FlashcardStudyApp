<template>
  <div class="list-cards">
    <div class="card teal-btn" 
        v-for="card in cards"  
        v-bind:key="card.cardId" 
        v-on:click="toggleFlip(card.cardId)"
        v-on:mouseover="currentCard = card.cardId" 
        v-on:mouseleave="currentCard = 0" >
        <div class="hover-btn">
        <img class="edit" src="../assets/settings.svg" v-show="currentCard == card.cardId" v-on:click="editCard(card)" />
        <img class="edit" src="../assets/close.svg" v-show="currentCard == card.cardId" v-on:click="deleteCard(currentCard)" />

      </div>
      <h3 v-if="!showBack.includes(card.cardId)">{{ card.cardFront }}</h3>
      <h4 v-else>{{ card.cardBack }}</h4>
    </div>
  </div>
</template>

<script>
import cardService from "../services/CardService";
export default {
  name: "list-cards",
  data() {
    return {
      cards: [],
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
      deleteCard(){

      },
      editCard(){

      }
  },
  created() {

    cardService.getCardsInDeck(parseInt(this.$route.params.deckId)).then((response) => {this.cards = response.data});
  },
};
</script>

<style>
.card {
  width: 350px;
  height: 200px;
  padding: 10px;
}
.list-cards{
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  margin-top: 40px;
}
a:hover{
  text-decoration: none;

}
.hover-btn{
  height: 40px;
  width: 100%;
  display: flex;
  justify-content: space-between;
}
.edit{
  width: 35px;
}
</style>