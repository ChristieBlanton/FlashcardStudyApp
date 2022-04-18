<template>
  <div class="deck-details">
    <h1>
      {{ deckName }}<br />
      <h4 v-if="$route.name != 'startstudysession'">{{ deckDescription }}</h4>
    </h1>
  </div>
</template>

<script>
import deckService from "../services/DeckService";
export default {
  name: "deck-details",
  data() {
    return {
      deckName: "",
      deckDescription: "",
    };
  },

  created() {
    deckService.getDeck(this.$route.params.deckId).then((response) => {
      this.deckName = response.data.deckName;
      this.deckDescription = response.data.deckDescription;
    });
  },
};
</script>

<style>
.deck-details {
  display: flex;
  justify-content: center;
  width: 100%;
}
</style>