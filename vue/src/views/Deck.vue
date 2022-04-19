<template>
    <div class="deck-details">
        <deck-details />

        <div class="add-study">
            <add-card />
            <!-- <button class="deck-start-study small-new-teal-btn">Study</button> -->

        </div>

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
    justify-content: space-around;
}
.deck-start-study{

}
</style>

