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
      currentUser: {
          userId: this.$store.state.user.userId,
      }
    };
  },
  methods: {
    getDecks() {
      
    },
  },
  created() {
      console.log(this.currentUser);

    deckService.myDecks(this.currentUser).then((response) => {this.decks = response.data});
  },
};
</script>

<style>
.deck {
  width: 100px;
  height: 100px;
  background-color: aquamarine;
}
</style>