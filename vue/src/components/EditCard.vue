<template>
    <div class="edit-card">
        

        <div class="edit-card-form">
            <label for="card-front">Front</label>
            <input type="text" name="card-front" id="card-front" v-model="card.cardFront">
            <label for="card-back">Back</label>
            <textarea rows="5" cols="40" name="card-back" id="card-back" v-model="card.cardBack"></textarea>
            <div class="tags">
                <label for="tags-basic">Type a new tag and press enter</label>
                <input type="text" v-on:keyup.enter="addTag()" v-model="tag">
                <p>Tags: </p>
                <div class="list-tags" v-for="tag in card.tags" v-bind:key="tag">
                    <p>{{ tag }} <img class="edit" src="../assets/close.svg" v-on:click="removeTag(tag)" /></p>

                </div>
            </div>
            <div class="form-btn">
                <button class="teal-btn" v-on:click="editCard()">Submit</button>
                <button class="teal-btn" v-on:click="$router.push({name: 'deck', params: {deckId: $route.params.deckId}})">Cancel</button>

            </div>
        </div>
    </div>
</template>

<script>
import cardService from "../services/CardService";

export default{
    name: 'edit-card',
    data(){
        return{
            tag: '',
            card: {
                
                cardId: 0,
                cardFront: "",
                cardBack: "",
                tags: []
            }
        }
    },
    methods: {
        editCard(){
            
            cardService.editCard(this.card).then(() => {
                this.$router.push({name: 'deck', params: {deckId: this.$route.params.deckId}});
            });
        },
        addTag(){
            this.card.tags.push(this.tag);
            this.tag = '';
        },
        removeTag(tag){
            this.card.tags = this.card.tags.filter(a => {
                return a != tag
            })
        }
    },
    created() {
        this.card.cardId = this.$store.state.editCard.cardId
        this.card.cardFront = this.$store.state.editCard.cardFront
        this.card.cardBack = this.$store.state.editCard.cardBack
        this.card.tags = this.$store.state.editCard.tags
    }
}
</script>

<style scoped>
.edit-card-btn{
    height: 40px;
    width: 200px;
}
.edit-card{
    display: flex;
    justify-content: center;
}
.edit-card-form{
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