<template>
  <div class="list-cards">
    <button class="card purple-btn btn btn-lg btn-primary btn-block" id="card"
        v-for="card in cards"  
        v-bind:key="card.cardId" 
        v-bind:value="card.cardId"
        v-on:click="toggleFlipAnimate(card.cardId); toggleFlip(card.cardId)"
        v-on:mouseover="currentCard = card.cardId" 
        v-on:mouseleave="currentCard = 0; "
         >
        <!-- <div class="card-inner" v-bind:class="{'flip-text': showBack.includes(card.cardId)}"> -->
          <!-- v-bind:class="{'flip-card': showBack.includes(card.cardId)}" -->
          <div class="hover-btn">
            <img class="edit" src="../assets/cog.svg" v-show="currentCard == card.cardId" v-on:click="editCard(card)" />
            <h3 class="list-cards-text" v-show="!showBack.includes(card.cardId)"></h3>
            <h3 class="list-cards-text" v-show="showBack.includes(card.cardId)"></h3>

            <img class="edit" src="../assets/close.svg" v-show="currentCard == card.cardId" v-on:click="deleteCard(currentCard)" />

          </div>
          <!-- <h3 class="list-cards-text" v-if="!showBack.includes(card.cardId)">{{ card.cardFront }}</h3>
          <h3 class="list-cards-text" v-else>{{ card.cardBack }}</h3> -->
          <div class="card-front-if-img" v-show="(!showBack.includes(card.cardId) && card.cardImage)">
            <h3 class="list-cards-text" >{{ card.cardFront }}</h3>
            <img id="card-image" :src="(card.cardImage)" alt="card image">

          </div>
            <h3 class="list-cards-text" v-show="!showBack.includes(card.cardId) && !card.cardImage">{{ card.cardFront }}</h3>

          <h3 class="list-cards-text" v-show="showBack.includes(card.cardId)">{{ card.cardBack }}</h3>

        <!-- </div> -->
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
      zoomCardImage: false,
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
  width: 30% !important;
  height: 20vh !important;
  display: flex;
  cursor: default !important;
  overflow: scroll;
  border-width: 3px;
  border-color: rgba(255, 255, 255, 0.301);
  border-style: solid;
  transition: .5s;
}

.card-front-if-img{
  display: flex;
  width: 100%;
  height: 100%;
  padding: 5px;
}
.card-front-if-img{
  text-align: center;
  text-justify: center;
}
#card-image {
  max-height: 12vh;
  width: auto;
  max-width: 50%;
  margin-bottom: 5px;
  border-color: rgba(255, 255, 255, 0.699);
  border-width: 3px;
  border-style: solid;
  border-radius: 8px;
  margin-left: 5px;
}

/* .card-img-zoom{
  height: 25vh !important;
  position:relative !important;
} */

button#card.card{
  align-items: center !important;

  padding: 0 8px!important;
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
.flip-card-animate{
  animation-name: flip-card-animate;
  animation-duration: .3s;
  animation-fill-mode:forwards;
}

@keyframes flip-card-animate{
    0% {transform: rotate3d(0);}
    50% {transform: rotate3d(1, .02, 0, 90deg);}
    100% {transform: rotate3d(0);}


}
.flip-text{
  transform: rotateX(180deg);

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