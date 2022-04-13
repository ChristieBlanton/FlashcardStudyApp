<template>
    <div class="edit-deck">
        
        <div class="edit-deck-form">
            <label for="deck-name">Deck Name: </label>
            <input type="text" name="deck-name" id="deck-name" v-model="deck.deckName">
            <label for="deck-description">Deck Description: </label>
            <textarea rows="5" cols="40" name="deck-description" id="deck-description" v-model="deck.deckDescription"></textarea>
            <div class="form-btn">
                <button class="teal-btn" v-on:click="editDeck()">Submit</button>
                <button class="teal-btn" v-on:click="$router.push({name: 'mydecks'})">Cancel</button>

            </div>
        </div>
    </div>
</template>

<script>
import deckService from "../services/DeckService";

export default{
    name: 'edit-deck',
    data(){
        return{
            isAddDeckVisible: false,
            deck: {
                deckId: 0,
                deckName: "",
                deckDescription: ""
            }
        }
    },
    methods: {
        editDeck(){
            
            deckService.editDeck(this.deck).then(() => {
                this.$router.push({name: "mydecks"});
            });
        }
    },
    created() {
        this.deck.deckId = this.$store.state.editDeck.deckId
        this.deck.deckName = this.$store.state.editDeck.deckName
        this.deck.deckDescription = this.$store.state.editDeck.deckDescription
    }
}
</script>

<style scoped>
.new-deck-btn{
    height: 40px;
    width: 200px;
}
.edit-deck{
    display: flex;
    justify-content: center;
}
.edit-deck-form{
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