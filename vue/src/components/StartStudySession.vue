<template>
    <div class="start-study-session">
        <div v-if="!endSession">
            <div class="current-flash-card card purple-btn" v-on:click="showBack = !showBack">
                <h3 class="current-card-front" v-if="!showBack">{{currentCard.cardFront}}</h3>
                <h3 class="current-card-back" v-else>{{currentCard.cardBack}}</h3>


            </div>
            <button class="mark-correct" v-if="showBack" v-on:click="markCorrect"></button>
            <button class="mark-incorrect" v-if="showBack" v-on:click="markIncorrect"></button>
            <button class="end-study-session-btn" v-on:click="endSession = true">End Study Session</button>

        </div>
        <div class="end-study-session" v-else>
            <h3>You got {{correct}} cards correct</h3>
            <h3>You got {{incorrect}} cards incorrect</h3>

        </div>
    </div>
</template>

<script>
import cardService from '../services/CardService'
export default {
    name: 'start-study-session',
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
.mark-correct{
    width: 50px;
    height: 50px;
    background-color: green;
}
.mark-incorrect{
    width: 50px;
    height: 50px;
    background-color: red;
}
</style>