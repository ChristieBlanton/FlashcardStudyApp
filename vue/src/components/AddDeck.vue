<template>
    <div class="add-deck">
        <button v-on:click="isAddDeckVisible = !isAddDeckVisible" v-if="!isAddDeckVisible">New Deck</button>

        <div v-if="isAddDeckVisible">
            <label for="deck-name">Deck Name</label>
            <input type="text" name="deck-name" id="deck-name" v-model="deck.deckName">
            <button v-on:click="addDeck()">Submit</button>
            <button v-on:click="isAddDeckVisible = false">Cancel</button>
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
                userId: this.$store.state.user.userId,
            }
        }
    },
    methods: {
        addDeck(){
            
            deckService.addDeck(this.deck);
            this.deck.deckName = "";
            this.isAddDeckVisible = !this.isAddDeckVisible;
        }
    },
}
</script>
