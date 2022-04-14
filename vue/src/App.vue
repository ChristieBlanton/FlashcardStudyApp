<template>
  <div id="app">
    <div id="nav">
      <router-link class="home" v-bind:to="{ name: 'home' }">Home</router-link>
      <router-link class="my-decks-btn" v-bind:to="{ name: 'mydecks' }">My Decks</router-link>
      <router-link class="logout" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <router-link class="register" v-bind:to="{ name: 'register' }" v-if="$store.state.token == ''">Register</router-link>
      <router-link class="login" v-bind:to="{ name: 'login' }" v-if="$store.state.token == ''">Login</router-link>
    </div>
    <router-view v-bind:class="{main: $route.path != {name: 'index'}}" />
  </div>
</template>

<style>
html, body, #app{
  height: 100%;
}
*{
  font-family:sans-serif;
}
#nav{
  display: flex;
  justify-content: space-between;
  font-size: 2em;
  padding: 0 20px 0 20px;
  background-color: aquamarine;
  
}
#nav a{
  text-decoration: none;
  color: rgb(71, 71, 71);
  font-weight: bold;
}
#nav a.router-link-exact-active {
  color: grey;
}
.main{
  background-color: whitesmoke;
  width: 80%;
  margin: auto;
  padding: 30px;
  box-shadow: 0 3px 5px lightgray;
}
.login, .register, .my-decks-btn, .logout{
  padding: 0 15px 0 15px;
}
.my-decks-btn, .mydecks {
  flex-grow: 1;
}
* > .teal-btn{
  background-color: aquamarine;
  border: none;
  box-shadow: -2px 3px 5px lightgray;
  display: flex;
  justify-content: center;
  align-items: center;
  flex-direction: column;
  color: black;
  border-radius: 5px;
}
* > .teal-btn:hover{
  background-color: rgb(70, 233, 178);
}

</style>
