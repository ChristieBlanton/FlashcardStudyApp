<template>
    <div class="deck-details">
            <h2>{{deckName}}<br><h4>{{deckDescription}}</h4></h2>
            
    </div>
</template>

<script>
export default {

    name: 'deck-details',
    data(){
        return {
            
        }
    },
    
    computed: {
        deckName(){
            if(this.$route.name == 'deck'){
                return this.$store.state.decks.filter(d => {
                    return d.deckId == this.$route.params.deckId;
                }).deckDescription
            }
            else{
                return 'hi';
            }
        }
    }
}
</script>

<style>
.deck-details{
    display: flex;
    justify-content: center;
    width: 100%;
}
</style>