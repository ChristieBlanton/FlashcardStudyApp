<template>
  <div class="home">
    <h2 class="home-header">Welcome to Flasher<br> <h3 class="home-header home-header-small">Your Flash Card Study App</h3></h2>
    <div class="home-options">
      <!-- <button class="navy-btn">Community Decks</button> -->
      <!-- <button class="purple-btn">Community Cards</button> -->
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

<style>
@import url('https://fonts.googleapis.com/css2?family=Pacifico&display=swap');

.home-header {
  color: #071E3D;
  font-family: 'Pacifico';
  font-size: 5vw;
  text-align: center;
}
.home-header-small{
  font-size: 3vw;
}


.home-options{
  display: flex;
  flex-wrap: wrap;
  justify-content: center;
  gap: 10px;
  margin-top: 40px;
}
a:hover{
  text-decoration: none;

}
</style>
