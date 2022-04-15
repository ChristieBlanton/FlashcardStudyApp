<template>
  <div id="app">
    <div class="app-overlay">

    <div id="header">
      
    </div>
    <div class="footer">

    </div>
    </div>



      <div class="left-head-nav">
        <router-link class="home-btn" v-bind:to="{ name: 'home' }"><img class="logo-nav" src="./assets/flasher-logo-gray-small.png" ></router-link>
        <router-link class="my-decks-btn" v-bind:to="{ name: 'mydecks' }" v-if="$store.state.token != ''">My Decks</router-link>
        <router-link class="my-cards-btn" v-bind:to="{ name: 'mycards' }" v-if="$store.state.token != ''">My Cards</router-link>

        <router-link class="logout" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>

      </div>
      <!-- <img class="menu-icon" src="./assets/menu.svg" /> -->
      


    <router-view class="main" v-bind:class="{view: $route.path != '/'&& $route.path != '/login' && $route.path != '/register'}" />

    

  </div>


</template>

<style>
html, body, #app{
  height: 100%;
}




@import url('https://fonts.googleapis.com/css2?family=Pacifico&display=swap');
h1{
  font-family: 'Pacifico';
  font-weight: 100;
}

.home-icon{
  width: 35px;
}
.menu-icon{
  width: 40px;
}
.logo-nav{
  width: 100px;
  
}
.logout{
  top: 25%;
}








.home-btn, .my-decks-btn, .logout, .my-cards-btn{
  position: fixed;
  font-size: 20px;
}
.home-btn{
  left: 3%;
  top: 3%;
}
.my-decks-btn{
  top: 20%;

}
#header{
  grid-area: nav;
  width: 25%;
  height: 150%;
  top:-20%;
  left: -20%;
  transform: rotate(13deg);
  position:fixed;
  box-shadow:  inset 0 0 10px  gray;
    box-shadow:  0 0 7px  gray;

}
.footer{
  grid-area: footer;
  background-color: #1ee3cf;
  width: 25%;
  height: 150%;
  top:-10%;
  left: 93%;
  transform: rotate(13deg);
  position:fixed;
  box-shadow:  inset 0 0 7px  gray;
  
}
.view{
  grid-area: view;
  width: 75% !important;
  margin: auto;
}
/* #app{
  display: grid;
  grid-template-columns: 1fr 5fr 1fr;
  grid-template-areas: 
    "nav view footer";
} */
#app{
  display: flex;
  position: relative;
}
.app-overlay{
  overflow: hidden;
  
  height: 100% !important;
}



*{
  font-family:sans-serif;
}
#header{
  /* display: flex;
  justify-content: space-between; */
  font-size: 2em;
  /* padding: 0 20px 0 20px; */
  background-color: #1ee3cf;
  
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
  padding: 30px;
  margin: auto;
}
.login, .register, .my-decks-btn, .logout{
  padding: 0 15px 0 15px;
}
/* .my-decks-btn, .home{
  flex-grow: 1;
} */
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
* > .new-teal-btn{
  background-color: #1ee3cf;
  width: 300px;
  height: 200px;
  padding: 20px;
  margin: 10px;
  border: none;
  border-radius: 20px;
  box-shadow: -2px 3px 5px lightgray;
  justify-content: center;
  align-items: center;
  color: black;
  font-size: 22px;
}
* > .new-teal-btn:hover{
  background-color: #1ac0af;
}
* > .small-new-teal-btn{
  background-color: #1ee3cf;
  padding: 15px;
  margin: 10px;
  border: none;
  border-radius: 10px;
  box-shadow: -2px 3px 5px lightgray;
  justify-content: center;
  align-items: center;
  color: black;
  font-size: 26px;

}
* > .small-new-teal-btn:hover{
  background-color: #1ac0af;
}
* > .purple-btn{
  background-color: #593cda;
  width: 300px;
  height: 200px;
  padding: 20px;
  margin: 10px;
  border: none;
  border-radius: 20px;
  box-shadow: -2px 3px 5px lightgray;
  justify-content: center;
  align-items: center;
  color: whitesmoke;
  font-size: 22px;
}
* > .purple-btn:hover{
  background-color: #4b32bb;
}
* > .small-purple-btn{
  background-color: #593cda;
  padding: 20px;
  margin: 10px;
  border: none;
  border-radius: 10px;
  box-shadow: -2px 3px 5px lightgray;
  justify-content: center;
  align-items: center;
  color: whitesmoke;
  font-size: 22px;
}
* > .small-purple-btn:hover{
  background-color: #4b32bb;
}
* > .lt-btn{
  background-color: #cacaca;
  width: 300px;
  height: 200px;
  padding: 20px;
  margin: 10px;
  border: none;
  border-radius: 20px;
  box-shadow: -2px 3px 5px lightgray;
  justify-content: center;
  align-items: center;
  color: black;
  font-size: 22px;
}
* > .lt-btn:hover{
  background-color: #afafaf;
}
* > .small-lt-btn{
  background-color: #cacaca;
  padding: 20px;
  margin: 10px;
  border: none;
  border-radius: 10px;
  box-shadow: -2px 3px 5px lightgray;
  justify-content: center;
  align-items: center;
  color: black;
  font-size: 22px;
}
* > .small-lt-btn:hover{
  background-color: #afafaf;
}
* > .navy-btn{
  background-color: #0D3F67;
  width: 300px;
  height: 200px;
  padding: 20px;
  margin: 10px;
  border: none;
  border-radius: 20px;
  box-shadow: -2px 3px 5px lightgray;
  justify-content: center;
  align-items: center;
  color: whitesmoke;
  font-size: 22px;
}
* > .navy-btn:hover{
  background-color: #092c49;
}
* > .small-navy-btn{
  background-color: #0D3F67;
  width: 300px;
  margin: 10px;
  border: none;
  border-radius: 10px;
  box-shadow: -2px 3px 5px lightgray;
  justify-content: center;
  align-items: center;
  color: whitesmoke;
  font-size: 22px;
}
* > .small-navy-btn:hover{
  background-color: #092c49;
}
</style>
