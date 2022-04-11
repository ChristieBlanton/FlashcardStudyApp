<template>
  <div id="app">
    <div id="nav">
      <router-link v-bind:to="{ name: 'home' }">Home</router-link>
      <router-link v-bind:to="{ name: 'mydecks' }" v-if="$store.state.token != ''">My Decks</router-link>
      <router-link v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
    </div>
    <router-view />
  </div>
</template>

<style>

#nav{
  display: flex;
  justify-content: space-evenly;
  width: 50%;
  margin: auto;
  font-size: 2em;
}
#nav a{
  text-decoration: none;

}

</style>
