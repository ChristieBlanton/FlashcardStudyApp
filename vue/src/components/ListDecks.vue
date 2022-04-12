<template>
  <div class="list-decks">
    <div class="deck teal-btn" v-on:mouseover="currentdeck = deck.deckId" v-on:mouseleave="currentdeck = 0" v-for="deck in decks" v-bind:key="deck.deckId" v-on:click.self="$router.push({name: 'deck', params: {deckId: deck.deckId}})">
      <h3 v-on:click.self="$router.push({name: 'deck', params: {deckId: deck.deckId}})">{{ deck.deckName }}</h3>
      <button v-show="currentdeck == deck.deckId" v-on:click="editDeck(deck)" class="hover-btn">Edit</button>
      <button v-show="currentdeck == deck.deckId" class="hover-btn">Delete</button>
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
    getDecks() {
      
    },
    editDeck(deck) {
      this.$store.commit("EDIT_DECK", deck)
      this.$router.push({name: "editdeck"})
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
  
  
  
}
.list-decks{
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  margin-top: 40px;
}
a:hover{
  text-decoration: none;

}
.hover-btn{
  margin: 0px;
}


/* <router-link class="list-decks" v-bind:to="{name: 'deck', params: {deckId: deck.deckId}}"> */
</style>