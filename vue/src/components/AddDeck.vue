<template>
    <div class="add-deck">
        <button class="new-deck-btn teal-btn" v-on:click="isAddDeckVisible = !isAddDeckVisible" v-if="!isAddDeckVisible">New Deck</button>

        <div class="add-deck-form" v-if="isAddDeckVisible">
            <input type="text" name="deck-name" id="deck-name" placeholder="Deck Name" v-model="deck.deckName">
            <div class="form-btn">
                <button class="teal-btn" v-on:click="addDeck()">Submit</button>
                <button class="teal-btn" v-on:click="isAddDeckVisible = false">Cancel</button>

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
                userId: this.$store.state.user.userId,
            }
        }
    },
    methods: {
        addDeck(){
            
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
