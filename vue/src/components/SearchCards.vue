<template>
<div class="search-cards">
        <div class="search-form">
            <input type="text" name="search-query" id="search-query" placeholder="Search Cards" v-model="searchQuery">
        </div>
        <div class="card teal-btn" 
            v-for="card in filteredCards"  
            v-bind:key="card.cardId" 
            v-on:click="toggleFlip(card.cardId)"
            v-on:mouseover="currentCard = card.cardId" 
            v-on:mouseleave="currentCard = 0" >
            <!-- <div class="hover-btn">
            <img class="edit" src="../assets/cog.svg" v-show="currentCard == card.cardId" v-on:click="editCard(card)" />
            <img class="edit" src="../assets/close.svg" v-show="currentCard == card.cardId" v-on:click="deleteCard(currentCard)" />

        </div> -->
        <h3 v-if="!showBack.includes(card.cardId)">{{ card.cardFront }}</h3>
        <h4 v-else>{{ card.cardBack }}</h4>
        <div class="card-tags" >
            <p class="tag-name" v-for="tag in card.tags" v-bind:key="tag">{{tag}}</p>
        </div>
        </div>
    </div>

</template>

<script>
import cardService from "../services/CardService";
export default {
    name: 'search-cards',
    data(){
        return{
            cards: [],
            currentCard: 0,
            showBack: [],
            searchQuery: "",
        }
    },
    methods: {
        toggleFlip(cardId){
          if(!this.showBack.includes(cardId)){
              this.showBack.push(cardId);
          }
          else{
              this.showBack = this.showBack.filter(a => {
                  return a != cardId;
              })
          }
      },
    },
    created(){
        cardService.getCardsByUser(this.$store.state.user.userId).then((response) => {this.cards = response.data});
    },
    computed: {
    filteredCards (){
      if(this.searchQuery){
      return this.cards.filter((card)=>{
        return card.tags.filter(tag => {
            return tag.startsWith(this.searchQuery)
        }).length > 0
      })
      }else{
        return this.cards;
      }
    }
  }
}
</script>

<style>
.card-tags{
    display: flex;
    gap: 5px;
}
.card {
  width: 350px;
  height: 200px;
  padding: 10px;
  display: flex;
  justify-content: flex-start;
}
.search-cards{
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
  margin-top: 40px;
}
a:hover{
  text-decoration: none;

}
.hover-btn{
  height: 40px;
  width: 100%;
  display: flex;
  justify-content: space-between;
}
.edit{
  width: 35px;
}
</style>