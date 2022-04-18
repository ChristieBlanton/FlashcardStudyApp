<template>
  <div class="choose-study-session">
    <div
      class="deck navy-btn"
      v-on:mouseover="currentdeck = deck.deckId"
      v-on:mouseleave="currentdeck = 0"
      v-for="deck in decks"
      v-bind:key="deck.deckId"
      v-on:click.self="chooseDeck(deck)"
    >
      
      <h3 class="listdecks-deck-text" v-on:click.self="chooseDeck(deck)">
        {{ deck.deckName }}
      </h3>
    </div>
  </div>
</template>

<script>
import deckService from "../services/DeckService";
export default {
  name: "choose-study-session",
  data() {
    
    return {
      currentdeck: 0
      
      
    };
  },
  methods: {
    chooseDeck(deck){
      this.$store.commit("EDIT_DECK", deck)
      this.$router.push({name: 'startstudysession', params: {deckId: deck.deckId}})
    },
    
  },
  created() {

    deckService.myDecks(this.$store.state.user.userId).then((response) => {this.$store.commit("SET_DECKS", response.data )});
  },
  computed: {
    decks(){
      return this.$store.state.decks
    }
  }
};
</script>