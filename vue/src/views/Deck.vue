<template>
    <div class="deck-details">
        <add-card />
        <list-cards />
    </div>
</template>

<script>
import ListCards from '../components/ListCards.vue'
import AddCard from '../components/AddCard.vue'
export default {
    name: 'deck',
    components: {
        AddCard,
        ListCards
    }
}
</script>