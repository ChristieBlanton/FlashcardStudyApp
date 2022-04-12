<template>
  <div class="list-cards">
    <div class="card teal-btn" v-for="card in cards"  v-bind:key="card.cardId" v-on:click="toggleFlip(card.cardId)">
      <h3 v-if="!showBack.includes(card.cardId)">{{ card.cardFront }}</h3>
      <h3 v-else>{{ card.cardBack }}</h3>
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

</style>