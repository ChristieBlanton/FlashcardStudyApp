<template>
    <div class="add-deck">
        <label for="deck-name">Deck Name</label>
        <input type="text" name="deck-name" id="deck-name" v-model="deckName">
        <button type="submit" v-on:submit="addDeck()">Submit</button>
    </div>
</template>

<script>
import deckService from "../services/DeckService";

export default{
    data(){
        return{
            deckName: "",
        }
    },
    methods: {
        addDeck(){
            deckService.addDeck(this.$store.state.user.userId, this.deckName);
        }
    }
}
</script>
