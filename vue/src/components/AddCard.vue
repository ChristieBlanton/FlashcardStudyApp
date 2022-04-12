<template>
    <div class="add-card">
        <button class="new-card-btn teal-btn" v-on:click="isAddCardVisible = !isAddCardVisible" v-if="!isAddCardVisible">New Card</button>

        <div class="add-card-form"  v-if="isAddCardVisible">
            <input type="text" name="card-front" id="card-front" placeholder="Card Front" v-model="card.cardFront">
            <input type="text" name="card-back" id="card-back" placeholder="Card Back" v-model="card.cardBack">
            <div class="form-btn">
                <button class="teal-btn" v-on:click="addCard()">Submit</button>
                <button class="teal-btn" v-on:click="isAddCardVisible = false">Cancel</button>

            </div>
        </div>
    </div>
</template>

<script>
import cardService from "../services/CardService";

export default{
    name: 'add-card',
    data(){
        return{
            isAddCardVisible: false,
            card: {
                cardFront: "",
                cardBack: "",
                deckId: parseInt(this.$route.params.deckId),
                userId: this.$store.state.user.userId,
            }
        }
    },
    methods: {
        addCard(){
            
            cardService.addCard(this.card).then(() => {
                this.card.cardFront = "";
                this.card.cardBack = "";
                this.isAddCardVisible = !this.isAddCardVisible;
                location.reload(true);
            });
            
        }
    },
}
</script>

<style>
.new-card-btn{
    height: 40px;
    width: 200px;
}
.add-card{
    display: flex;
    justify-content: center;
}
.add-card-form{
    display: flex;
    flex-direction: column;
}
#card-name{
    margin-bottom: 5px;
}
.form-btn{
    display: flex;
    justify-content: center;
    gap: 2px;
}
.form-btn > *{
    box-shadow: none;
    width: 50%;
    margin: 0;
    font-size: 20px;

}

</style>