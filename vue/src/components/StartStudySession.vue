<template>
    <div class="start-study-session">
        <deck-details class="study-deck-name" />
        <div class="study-session" v-if="!endSession">
            <button class="current-flash-card card purple-btn" v-on:click="showBack = !showBack">
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
            <h3>You got {{correct}} cards correct</h3>
            <h3>You got {{incorrect}} cards incorrect</h3>

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
            currentCard: {},
            currentCardIndex: 0,
            showBack: false,
            correct: 0,
            incorrect: 0,
            endSession: false
        }
    },
    methods: {
        markCorrect(){
            this.correct ++;
            if(this.cards.length > this.currentCardIndex + 1){
                this.currentCardIndex ++;
                this.currentCard = this.cards[this.currentCardIndex];
                this.showBack = false;
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
            }
            else{
                this.endSession = true;
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
.study-session{
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