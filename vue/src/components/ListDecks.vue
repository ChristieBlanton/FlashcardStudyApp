<template>
  <div class="list-decks">
    <div class="deck" v-for="deck in decks" v-bind:key="deck.deckId">
      <h3>{{ deck.deckName }}</h3>
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
      
    };
  },
  methods: {
    getDecks() {
      
    },
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
  background-color: aquamarine;
  box-shadow: -2px 3px 5px lightgray;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  
}
.list-decks{
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
}
</style>