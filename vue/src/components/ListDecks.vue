<template>
  <div class="list-decks">
    <router-link class="list-decks" v-bind:to="{name: 'deckdetails'}"><div class="deck teal-btn" v-for="deck in decks" v-bind:key="deck.deckId">
      <h3>{{ deck.deckName }}</h3>
    </div></router-link>
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

</style>