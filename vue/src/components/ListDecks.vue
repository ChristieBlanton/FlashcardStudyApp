<template>
  <div class="list-decks">
    <div class="deck teal-btn" 
      v-on:mouseover="currentdeck = deck.deckId" 
      v-on:mouseleave="currentdeck = 0" 
      v-for="deck in decks" 
      v-bind:key="deck.deckId" 
      v-on:click.self="viewDeck(deck)">
      <div class="hover-btn">
        <img class="edit" src="../assets/settings.svg" v-show="currentdeck == deck.deckId" v-on:click="editDeck(deck)" />
        <img class="edit" src="../assets/close.svg" v-show="currentdeck == deck.deckId" v-on:click="deleteDeck(currentdeck)" />

      </div>
      <h3 v-on:click.self="viewDeck(deck)">{{ deck.deckName }}</h3>
    </div>
  </div>
</template>

<script>
import deckService from "../services/DeckService";
export default {
  name: "list-decks",
  data() {
    
    return {
      decks: [],
      showbutton: false,
      currentdeck: 0
      
      
    };
  },
  methods: {
    viewDeck(deck){
      this.$store.commit("EDIT_DECK", deck)
      this.$router.push({name: 'deck', params: {deckId: deck.deckId}})
    },
    editDeck(deck) {
      this.$store.commit("EDIT_DECK", deck)
      this.$router.push({name: "editdeck"})
    },
    deleteDeck(id){
      deckService.deleteDeck(id).then(() => {
        location.reload()
      });
    }
  },
  created() {

    deckService.myDecks(this.$store.state.user.userId).then((response) => {this.decks = response.data});
  },
};
</script>

<style>
.deck {
  width: 350px;
  height: 200px;
  padding: 10px;
  
  
}
button {
  background-color: grey;
}
.list-decks{
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 10px;
  margin-top: 40px;
}
a:hover{
  text-decoration: none;

}
h3{
  flex-grow: 1;
}
.hover-btn{
  height: 40px;
  width: 100%;
  display: flex;
  justify-content: space-between;
}
.edit{
  width: 40px;
}


/* <router-link class="list-decks" v-bind:to="{name: 'deck', params: {deckId: deck.deckId}}"> */
</style>