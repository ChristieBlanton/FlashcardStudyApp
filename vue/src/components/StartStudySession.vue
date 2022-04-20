<template>
  <div class="start-study-session">
    <form
      class="session-form"
      v-on:submit.prevent="
        timer = timerInput;
        studyTimer();
        startSession = true;
        onSubmit();
      "
      v-if="!startSession"
    >
      <button
        class="small-purple-btn skew-btn"
        id="isTimed"
        v-bind:class="{ 'session-form-selected': isTimed }"
        v-on:click.prevent="isTimed = !isTimed"
      >
        <div>Add Timer</div>
      </button>
      <!-- <label for="isTimed">Would you like to time your study session?</label> -->
      <label for="timer" v-if="isTimed">Seconds Per Card</label>
      <input
        type="number"
        id="timer"
        placeholder="Time per card in seconds"
        v-model="timerInput"
        v-if="isTimed"
      />
      <!-- <input type="checkbox" id="isRandom" v-model="isRandom"> -->
      <button
        class="small-purple-btn skew-btn"
        v-bind:class="{ 'session-form-selected': isRandom }"
        id="isRandom"
        v-on:click.prevent="isRandom = !isRandom"
      >
        <div>Randomize Cards</div>
      </button>


      <!-- <label for="isRandom">Would you like to randomize the order of the cards in the deck?</label> -->
      <button v-on:click="studyTimer()" class="session-form-submit small-navy-btn skew-btn" type="submit">
        <div>Begin Session</div>
      </button>
    </form>
    <div id="show-cards" v-else>
      <deck-details class="study-deck-name" />
      <div class="card-timer small-navy-btn skew-btn" v-if="isTimed">
        <div>{{ timer }}</div>
        

      </div>
      <div class="card-timer small-navy-btn skew-btn" v-if="isTimed">
        <div>{{ timer2 }}</div>
        

      </div>

      <div class="current-study-session" v-if="!endSession">
        <button
          class="current-flash-card card purple-btn"
          v-on:click="showBack = !showBack"
        >
          <h3 class="current-card-front" v-if="!showBack">
            {{ currentCard.cardFront }}
          </h3>
          <h3 class="current-card-back" v-else>{{ currentCard.cardBack }}</h3>
        </button>
        <div class="answer-btns">
          <button
            class="mark-incorrect skew-btn small-purple-btn"
            v-if="showBack"
            v-on:click.prevent="markIncorrect"
          >
            <div>Incorrect</div>
          </button>

          <button
            class="mark-correct skew-btn small-new-teal-btn"
            v-if="showBack"
            v-on:click.prevent="markCorrect"
          >
            <div>Correct</div>
          </button>
        </div>
        <button
          class="end-study-session-btn small-navy-btn skew-btn"
          v-on:click="endSession = true"
        >
          <div>End Study Session</div>
        </button>
      </div>

      <div class="end-study-session" v-else>
        <button class="session-result deck new-teal-btn"><div><h3>{{ correct }}</h3></div></button>
        <button class="session-result deck navy-btn"><div><h3>{{ incorrect }}</h3></div></button>

      </div>
    </div>
  </div>
</template>

<script>
import cardService from "../services/CardService";
import DeckDetails from "./DeckDetails.vue";
export default {
  name: "start-study-session",
  components: {
    DeckDetails,
  },
  data() {
    return {
      cards: [],
      randomCards: [],
      currentCard: {},
      currentCardIndex: 0,
      showBack: false,
      correct: 0,
      incorrect: 0,
      endSession: false,
      timer: 0,
      // timer2: 0,
      isTimer1: true,
      timerInput: 0,
      isTimed: false,
      isRandom: false,
      startSession: false,
    };
  },
  methods: {
    markCorrect() {
      this.correct++;
      if (this.cards.length > this.currentCardIndex + 1) {
        this.currentCardIndex++;
        this.currentCard = this.cards[this.currentCardIndex];
        this.showBack = false;
        this.timer = this.timerInput;
        
        // if(this.isTimer1){
        //         this.timer = 0;
        //         this.timer2 = this.timerInput;
        //         this.isTimer1 = false;
        //         this.studyTimer2();
        //     }
        //     else{
        //         this.timer2 = 0;
        //         this.timer = this.timerInput;
        //         this.isTimer1 = true;
        //         this.studyTimer();
        //     }

        // else{
        //     this.timer = this.timerInput;
            
        //     this.studyTimer();
        // }
      } else {
        this.endSession = true;
      }
    },
    markIncorrect() {
      this.incorrect++;
      if (this.cards.length > this.currentCardIndex + 1) {
        this.currentCardIndex++;
        this.currentCard = this.cards[this.currentCardIndex];
        this.showBack = false;
        this.timer = this.timerInput;
            // if(this.isTimer1){
            //     this.timer = 0;
            //     this.timer2 = this.timerInput;
            //     this.isTimer1 = false;
            //     this.studyTimer2();
            // }
            // else{
            //     this.timer2 = 0;
            //     this.timer = this.timerInput;
            //     this.isTimer1 = true;
            //     this.studyTimer();
            // }

        
        // else{
        //     this.timer = this.timerInput;
            
        //     this.studyTimer();
        // }
      } else {
        this.endSession = true;
      }
    },
    studyTimer() {
      if (this.isTimed === true) {
        if (this.timer > 0) {
          setTimeout(() => {
            this.timer -= 1;
            this.studyTimer();
          }, 1000);
        } 
        
        else {
          this.markIncorrect();
        }
      }
    },
    // studyTimer2() {
    //   if (this.isTimed === true) {
    //     if (this.timer2 > 0) {
    //       setTimeout(() => {
    //         this.timer2 -= 1;
    //         this.studyTimer2();
    //       }, 1000);
    //     } 
        
    //     else {
    //       this.markIncorrect();
    //     }
    //   }
    // },
    randomizeDeck(cards){
            if(this.isRandom === true){
                for( var i = cards.length; --i;) {
                    var j = Math.floor( Math.random() * (i+1));
                    var temp = cards[i]; 
                    cards[i] = cards[j];
                    cards[j] = temp;
    }
            }
                    this.randomCards = cards;

        },
    
        
    
    onSubmit() {
      if (this.isRandom) {
        this.randomizeDeck(this.cards);
      }
    },
  },
  created() {
    cardService.getCardsInDeck(this.$route.params.deckId).then((response) => {
      this.cards = response.data;
      this.currentCard = this.cards[0];
    });
  },
};
</script>

<style>
.session-result{
    transform: rotateX(90deg);
    animation-name: session-result-animate;
    animation-duration: .4s;
    animation-fill-mode: forwards;
}
.session-result div h3{
    font-size: 6vh;
}
@keyframes session-result-animate {
    from{transform: rotateX(90deg);}
    to{transform: rotateX(0deg);}
}
    
.session-form {
  display: flex;
  flex-direction: column;
  gap: 20px;
}
.session-form button {
  height: 50px;
  padding: 0;
}
.session-form-submit {
  height: 70px !important;
}
.session-form label {
  text-align: center;
}
.session-form-selected {
  /* border-color: aqua;
    border-width: 4px;
    border-style:outset; */
  box-shadow: 0 0 2px 5px aqua;
}
.study-deck-name {
  flex-grow: 1;
}
.card-timer {
  text-align: center;
  width: 10%;
  margin: auto;
}
.current-study-session {
  display: flex;
  flex-direction: column;
  justify-content: space-between;
  align-items: center;
  height: 30vh;
  flex-grow: 2;
}
.start-study-session {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}
.end-study-session-btn {
  margin-bottom: 40px;
  height: 50px;
  padding: 5px 30px;
}
.end-study-session {
  flex-grow: 3;
}
.answer-btns {
  flex-grow: 1;
  width: 50%;
  display: flex;

  justify-content: center;
}
#show-cards{
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    height: 100%;
}
.current-flash-card {
  height: 30vh;
  width: 30vw;
  box-shadow: 0px 3px 8px gray;
  display: flex;
}
.mark-correct,
.mark-incorrect {
  height: 5vh;
  padding: 0 15px;
}

.skew-btn {
  transform: skew(-13deg);
}
.skew-btn > * {
  transform: skew(13deg);
}
</style>