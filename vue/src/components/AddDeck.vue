<template>
    <div class="add-deck">
        <button class="new-deck-btn small-new-teal-btn" v-on:click="isAddDeckVisible = !isAddDeckVisible" v-if="!isAddDeckVisible">New Deck</button>

        <div class="add-deck-form" v-if="isAddDeckVisible">
            <input type="text" name="deck-name" id="deck-name" placeholder="Deck Name" v-model="deck.deckName">
            <textarea rows="5" cols="40" name="deck-description" id="deck-description" placeholder="Deck Description" v-model="deck.deckDescription"></textarea>
            <div class="form-btn">
                <button class="small-new-teal-btn" v-on:click="addDeck()">Submit</button>
                <button class="small-lt-btn" v-on:click="isAddDeckVisible = false">Cancel</button>

            </div>
        </div>
    </div>
</template>

<script>
import deckService from "../services/DeckService";

export default{
    name: 'add-deck',
    data(){
        return{
            isAddDeckVisible: false,
            deck: {
                deckName: "",
                deckDescription: "",
                userId: this.$store.state.user.userId,
            }
        }
    },
    methods: {
        addDeck(){
            
            deckService.addDeck(this.deck).then(() => {
                deckService.myDecks(this.$store.state.user.userId).then((response) => {this.$store.commit("SET_DECKS", response.data )});
                this.deck.deckName = "";
                this.isAddDeckVisible = !this.isAddDeckVisible;
                
            });
            
        }
    },
}
</script>

<style>
.new-deck-btn{
    width: 200px;
}
.add-deck{
    display: flex;
    justify-content: center;
}
.add-deck-form{
    display: flex;
    flex-direction: column;
}
#deck-name{
    margin-bottom: 5px;
}
.form-btn{
    display: flex;
    justify-content: center;
    gap: 2px;
}
.form-btn > *{
    box-shadow: none;
    width: 50%;
    margin: 0;
    font-size: 20px;

}

</style>
