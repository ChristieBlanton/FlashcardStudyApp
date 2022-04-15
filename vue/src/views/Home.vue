<template>
  <div class="home">
    <h1 id="home-header">Welcome to Flasher: Your Flash Card Study App</h1>
    <div class="home-options">
      <button class="navy-btn">Community Decks</button>
      <button class="purple-btn">Community Cards</button>
      <button class="new-teal-btn" v-on:click="$router.push({name: 'mydecks'})">Your Decks</button>
      <button class="lt-btn" v-on:click="$router.push({name: 'mycards'})">Your Cards</button>

    </div>
  </div>
</template>

<script>

export default {
  name: "home",
  data() {
    return {
      
    }
  }
};
</script>

<style scoped>
#home-header {
  font-weight: bold;
  color: #071E3D;
}


.home-options{
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  margin-top: 40px;
}
a:hover{
  text-decoration: none;

}
