<template>
  <div id="app">
    <div id="header">
      
    </div>
    
    <router-link class="home-btn" v-bind:to="{ name: 'home' }"><img class="logo-nav" src="./assets/flasher-logo-gray-small.png" ></router-link>
      <router-link class="my-decks-btn" v-bind:to="{ name: 'mydecks' }" v-if="$store.state.token != ''"><img class="menu-icon" src="./assets/menu.svg" />My Decks</router-link>
      <router-link class="logout" v-bind:to="{ name: 'logout' }" v-if="$store.state.token != ''">Logout</router-link>
      <router-link class="register" v-bind:to="{ name: 'register' }" v-if="$store.state.token == '' && $route.path == '/'">
        <div class="fade-button"><div class="register-text">Register</div></div>
      </router-link>
      <router-link class="login" v-bind:to="{ name: 'login' }" v-if="$store.state.token == '' && $route.path == '/'">
        <div class="fade-button"><div class="login-text">Login</div></div></router-link>
    <router-view class="main" v-bind:class="{view: $route.path != '/'&& $route.path != '/login' && $route.path != '/register'}" />

    <div class="footer">

    </div>

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
  position: absolute;
  top: 25%;
}
.login{
  top: 57%;
  left: 40.5%;
  
}

.register{
  top: 65%;
  left: 40%;
}
.register, .login{
  background-color: rgba(127, 255, 212, 0);
  width: 350px;
  height: 50px;
  display: flex;
  align-items: center;
  justify-content: center;
  overflow: hidden;
  position: relative;
}
.login-text, .register-text{
  transform: rotate(-13deg);
  font-size: 20px;
  font-weight: bold;
  color: rgb(48, 48, 48);
}
.fade-button{
  
  background-color: aquamarine;
  height: 250%;
  width: 100%;
  position: relative;
  transform: rotate(13deg);
  display: flex;
  justify-content: center;
  align-items: center;
}
.home-btn, .my-decks-btn, .logout, .register, .login{
  position: absolute;
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
  position:absolute;
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
  position:absolute;
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
  overflow: hidden;
  position: relative;
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
* > .purple-btn{
  background-color: #6B48FF;
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
  background-color: #5a3dda;
}
* > .lt-btn{
  background-color: #F2F4F6;
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
  background-color: #babbbb;
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
</style>
