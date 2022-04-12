<template>
    <div class="edit-deck">
        
        <div class="edit-deck-form">
            <label for="deck-name">Deck Name: </label>
            <input type="text" name="deck-name" id="deck-name" v-model="deck.deckName">
            <div class="form-btn">
                <button class="teal-btn" v-on:click="editDeck()">Submit</button>
                <button class="teal-btn" v-on:click="isAddDeckVisible = false">Cancel</button>

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
                deckName: this.$store.state.editDeck.deckName,
                
            }
        }
    },
    methods: {
        editDeck(){
            
            deckService.addDeck(this.deck).then(() => {
                this.deck.deckName = "";
                this.isAddDeckVisible = !this.isAddDeckVisible;
                location.reload();
            });
        }
    },
}
</script>

<style>
.new-deck-btn{
    height: 40px;
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