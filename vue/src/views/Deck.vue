<template>
    <div class="deck-details">
        <deck-details />

        <!-- <div class="add-study"> -->
            <add-card />

        <!-- </div> -->

        <list-cards />
    </div>
</template>

<script>
import ListCards from '../components/ListCards.vue'
import AddCard from '../components/AddCard.vue'
import DeckDetails from '../components/DeckDetails.vue'
export default {
    name: 'deck',
    
    components: {
        AddCard,
        ListCards, 
        DeckDetails,
    },
    
}
</script>

<style>
.add-study{
    display: flex;
    justify-content: space-between;
    margin-left: 2vw;
    margin-right: 2vw;
}
.deck-start-study{
    height: 50px;
    padding: 0 30px;
}
</style>

