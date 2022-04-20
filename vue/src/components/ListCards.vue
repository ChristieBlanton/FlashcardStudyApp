<template>
  <div class="list-cards">
    <button class="card purple-btn btn btn-lg btn-primary btn-block" id="card"
        v-for="card in cards"  
        v-bind:key="card.cardId" 
        v-on:click="toggleFlip(card.cardId); flipCard(card.cardId)"
        v-on:mouseover="currentCard = card.cardId" 
        v-on:mouseleave="currentCard = 0"
        v-bind:class="{'flip-card': showBack.includes(card.cardId)}" >
        <div class="card-inner" v-bind:class="{'flip-card': showBack.includes(card.cardId)}">
          <div class="hover-btn">
            <img class="edit" src="../assets/cog.svg" v-show="currentCard == card.cardId" v-on:click="editCard(card)" />
            <h3 class="list-cards-text" v-show="!showBack.includes(card.cardId)">Front</h3>
            <h3 class="list-cards-text" v-show="showBack.includes(card.cardId)">Back</h3>

            <img class="edit" src="../assets/close.svg" v-show="currentCard == card.cardId" v-on:click="deleteCard(currentCard)" />

          </div>
          <!-- <h3 class="list-cards-text" v-if="!showBack.includes(card.cardId)">{{ card.cardFront }}</h3>
          <h3 class="list-cards-text" v-else>{{ card.cardBack }}</h3> -->
          <h3 class="list-cards-text" v-show="!showBack.includes(card.cardId)">{{ card.cardFront }}</h3>
          <h3 class="list-cards-text" v-show="showBack.includes(card.cardId)">{{ card.cardBack }}</h3>

        </div>
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
      // flipCard(cardId){
      //   if(this.currentCard == cardId){}
      // },
    //   
    deleteCard(cardId) {
      cardService.deleteCard(this.$route.params.deckId, cardId).then(() => {
        cardService
          .getCardsInDeck(parseInt(this.$route.params.deckId))
          .then((response) => {
            this.$store.commit("SET_CARDS", response.data);
          });
      });
    },
    editCard(card) {
      this.$store.commit("EDIT_CARD", card);
      this.$router.push({
        name: "editcard",
        params: { deckId: this.$route.params.deckId, cardId: this.currentCard }
      });
    },
  },
  created() {
    cardService
      .getCardsInDeck(parseInt(this.$route.params.deckId))
      .then((response) => {
        this.$store.commit("SET_CARDS", response.data);
      });
  },
  computed: {
    cards() {
      return this.$store.state.cards;
    },
  },
};
</script>

<style>
.card {
  width: 18vw;
  height: 20vh;
  display: flex;
  cursor: default !important;
  overflow: scroll;
  border-width: 3px;
  border-color: rgba(255, 255, 255, 0.301);
  border-style: solid;
  transition: .5s;
}

button#card.card{
  justify-content: center !important;
  align-items: center !important;
  padding: 0 !important;
}
.card-inner{
  height: 100%;
  width: 100%;
  padding: 5px;
  transition-delay: .2s;
  transition-duration: 10ms;
}
.flip-card{
  transform: rotateX(180deg);
  box-shadow: 0 -3px 5px gray !important;

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
  width: 0px;
  height: 0;
}
.card::-webkit-scrollbar-corner {
  opacity: 0;
}
.list-cards {
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  align-items: center;
  gap: 10px;
  margin-top: 40px;
}
a:hover {
  text-decoration: none;
}
.list-cards-text {
  pointer-events: none;
}
.hover-btn {
  height: 40px;
  width: 100%;
  display: flex;
  justify-content: space-between;
}
h3.list-cards-text{
  font-size: 2.2vh;
  opacity: 0;
  animation-name: cards-text-flip;
  animation-duration: .2s;
  animation-delay: .2s;
  animation-fill-mode: forwards;
}
@keyframes cards-text-flip {
  from{opacity: 0;}
  to{opacity: 1;}
}
.edit{
  width: 35px;
  cursor: pointer;
  animation-name: hover-btn-fade;
  animation-duration: .4s;
}
@keyframes hover-btn-fade {
  from{opacity: 0;}
  to{opacity: 1;}
}
.flip-card {
  width: 300px;
  height: 200px;
  perspective: 1000px; 
  
}

.flip-card-inner {
  position: relative;
  width: 100%;
  height: 100%;
  text-align: center;
  transition: transform 0.8s;
  transform-style: preserve-3d;
}

.flip-card.clicked .flip-card-inner {
  transform: rotateY(-180deg);
}


.flip-card-front, .flip-card-back {
  position: absolute;
  width: 100%;
  height: 100%;
  -webkit-backface-visibility: hidden;
  backface-visibility: hidden;
}


.flip-card-back {
  transform: rotateY(-180deg);
}
</style>