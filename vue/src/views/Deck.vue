<template>
    <div class="deck-details">
        <!-- <deck-details /> -->
        <add-card />
        <list-cards />
    </div>
</template>

<script>
import ListCards from '../components/ListCards.vue'
import AddCard from '../components/AddCard.vue'
// import DeckDetails from '../components/DeckDetails.vue'
export default {
    name: 'deck',
    
    components: {
        AddCard,
        ListCards, 
        // DeckDetails,
    },
    
}
</script>

