<template>
    <div class="start-study-session">
        <form  v-on:submit.prevent="studyTimer(); timer = timerInput; startSession = true; randomizeDeck(cards); onSubmit();" v-if="!startSession">
        <input type="checkbox" id="isTimed" v-model="isTimed">
        <label for="isTimed">Would you like to time your study session?</label>
        <label for="timer" v-if="isTimed"> Time per Card:</label>
        <input type="number" id="timer" v-model="timerInput" v-if="isTimed">
        <input type="checkbox" id="isRandom" v-model="isRandom">
        <label for="isRandom">Would you like to randomize the order of the cards in the deck?</label>
        <button type="submit" >Begin Session</button>
        <div>{{timer}}</div>
        </form >
        <div id="show-cards" v-else>
        <div v-if="isTimed">{{timer}}</div>
        <deck-details class="study-deck-name" />
        <div class="current-study-session" v-if="!endSession">
            <button class="current-flash-card card purple-btn" v-on:click="showBack = !showBack" v-if="!isRandom">
                <h3 class="current-card-front" v-if="!showBack">{{currentCard.cardFront}}</h3>
                <h3 class="current-card-back" v-else>{{currentCard.cardBack}}</h3>


            </button>

            <button class="current-flash-card card purple-btn" v-on:click="showBack = !showBack" v-else>
                <h3 class="current-card-front" v-if="!showBack">{{currentCard.cardFront}}</h3>
                <h3 class="current-card-back" v-else>{{currentCard.cardBack}}</h3>


            </button>

            <div class="answer-btns">
                <button class="mark-incorrect skew-btn small-purple-btn" v-if="showBack" v-on:click="markIncorrect"><div>Incorrect</div></button>

                <button class="mark-correct skew-btn small-new-teal-btn" v-if="showBack" v-on:click="markCorrect"><div>Correct</div></button>

            </div>
                <button class="end-study-session-btn small-navy-btn skew-btn" v-on:click="endSession = true"><div>End Study Session</div></button>


        </div>
        
        <div class="end-study-session" v-else>
            <h3>Number of cards correct: {{correct}}</h3>
            <h3>Number of cards incorrect: {{incorrect}}</h3>

        </div>
        </div>
    </div>
</template>

<script>
import cardService from '../services/CardService'
import DeckDetails from './DeckDetails.vue'
export default {
    name: 'start-study-session',
    components: {
        DeckDetails,
    },
    data(){
        return {
            cards: [],
            randomCards: [],
            currentCard: {},
            currentCardIndex: 0,
            showBack: false,
            correct: 0,
            incorrect: 0,
            endSession: false,
            timer: 10,
            timerInput: 0,
            isTimed: false,
            isRandom: false,
            startSession: false,

            
        }
    },
    methods: {
        markCorrect(){
            this.correct ++;
            if(this.cards.length > this.currentCardIndex + 1){
                
                this.currentCardIndex ++;

                if(this.isRandom){
                    this.currentCard = this.randomCards[this.currentCardIndex];
                }
                else{
                    this.currentCard = this.cards[this.currentCardIndex];

                }
                this.showBack = false;
                this.timer = this.timerInput;
                this.studyTimer();
            }
            else{
                this.endSession = true;
            }
        },
        markIncorrect(){
            this.incorrect ++;
            if(this.cards.length > this.currentCardIndex + 1){
                this.currentCardIndex ++;
                this.currentCard = this.cards[this.currentCardIndex];
                this.showBack = false;
                this.timer = this.timerInput;
                this.studyTimer();
            }
            else{
                this.endSession = true;
            }
        },
        studyTimer() {
            if(this.isTimed === true){
                 
                if(this.timer > 0) {
                    setTimeout(() => {
                        this.timer -= 1
                        this.studyTimer()
                    }, 1000)
                }else{
                    this.markIncorrect();
                }
            }
            },
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
        onSubmit(){
            if(this.isRandom){

                this.currentCard = this.randomCards[0];
            }
        },
        
    },
    created(){
        cardService.getCardsInDeck(this.$route.params.deckId).then(response => {
            this.cards = response.data;
            this.currentCard = this.cards[0]
        })
    }
}
</script>

<style>
.study-deck-name{
    flex-grow: 1;
}
.current-study-session{
    display: flex;
    flex-direction: column;
    justify-content: space-around;
    align-items: center;
    height: 30vh;
    flex-grow: 2;
}
.start-study-session{
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
}
.end-study-session-btn{
    margin-bottom: 40px;
    height: 50px;
    padding: 5px 30px;
}
.end-study-session{
    flex-grow: 3;

}
.answer-btns{
    flex-grow: 1;
    width: 50%;
    display: flex;

    justify-content: center;
}

.current-flash-card{
    height: 30vh;
    width: 30vw;
    box-shadow: 0px 3px 8px gray;
    display: flex;
}
.mark-correct, .mark-incorrect{
    height: 5vh;
    padding: 0 15px;
}



.skew-btn{
    transform: skew(-13deg);
}
.skew-btn > *{
    transform: skew(13deg);
}
</style>