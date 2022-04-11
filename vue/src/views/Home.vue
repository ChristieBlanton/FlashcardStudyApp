<template>
  <div class="home">
    <h1>Home</h1>
    <p>Welcome to your new home.</p>
    <p>
      In the initial configuration, this page can only be seen when logged in.
    </p>
    <add-deck />
  </div>
</template>

<script>
import AddDeck from "../components/AddDeck.vue"

export default {
  name: "home",
  components: {
    AddDeck,
  }
};
</script>
