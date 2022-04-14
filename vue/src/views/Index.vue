<template>
    <div class="main index">
        <div class="logo-text">
            <img class="logo" src="../assets/flasher-logo.png" >
            <h1>Flasher</h1>
        </div>
    </div>
</template>

<style>
.index{
    background-image: url("../assets/wrtiting-tools.jpg");
    background-repeat: no-repeat;
    background-size: cover;
}
.main{
    width: 100%;
    height: 100%;
    display: flex;
    justify-content: flex-start;
    flex-direction: column;
}
.logo{
  width: 350px;
  margin: auto;
}
.logo-text{
    display: flex;
    flex-direction: column;
    align-items: center;
}
h1{
    font-size: 100px;
    font-weight: 100;
}
</style>