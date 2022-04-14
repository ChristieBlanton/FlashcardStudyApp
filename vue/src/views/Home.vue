<template>
  <div class="home">
    <h1 id="home-header">Welcome to Flasher: Your Flash Card Study App</h1>
    <div class="home-options">
      <button class="navy-btn">Community Decks</button>
      <button class="purple-btn">Community Cards</button>
      <button class="new-teal-btn" v-on:click="$router.push({name: 'mydecks'})">Your Decks</button>
      <button class="lt-btn">Your Cards</button>

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
  text-align: center;
  width: 100%;
}
* > .new-teal-btn{
  background-color: #1ac0af;
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

.home-options{
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  margin-top: 40px;
}
a:hover{
  text-decoration: none;

}
