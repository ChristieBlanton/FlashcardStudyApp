<template>
    <div class="add-card">
        
        <button class="new-card-btn teal-btn" v-on:click="isAddCardVisible = !isAddCardVisible" v-if="!isAddCardVisible">New Card</button>

        <div class="add-card-form"  v-if="isAddCardVisible">
            <input type="text" name="card-front" id="card-front" placeholder="Card Front" v-model="card.cardFront">
            <textarea rows="5" cols="40" name="card-back" id="card-back" placeholder="Card Back" v-model="card.cardBack"></textarea>
            <div class="tags">
                <label for="tags-basic">Type a new tag and press enter</label>
                <!-- <b-form-tags input-id="tags-basic" v-model="card.tags"></b-form-tags>
                <p class="mt-2">Value: {{ card.tags }}</p> -->
                <input type="text" v-on:keyup.enter="addTag()" v-model="tag">
                <p>Tags: </p>
                <div class="list-tags" v-for="tag in card.tags" v-bind:key="tag">
                    <p>{{ tag }} </p>

                </div>
            </div>
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
            deckDescription: '',
            tag: '',
            card: {
                cardFront: "",
                cardBack: "",
                deckId: parseInt(this.$route.params.deckId),
                userId: this.$store.state.user.userId,
                tags: []
            }
        }
    },
    methods: {
        addCard(){
            
            cardService.addCard(this.card).then(() => {
                this.card.cardFront = "";
                this.card.cardBack = "";
                this.isAddCardVisible = !this.isAddCardVisible;
                this.card.tags = [];
                location.reload(true);
            });
            
        },
        addTag(){
            this.card.tags.push(this.tag);
            this.tag = '';
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