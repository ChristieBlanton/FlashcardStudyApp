<template>
    <div class="study-session">
        <choose-study-session />
    </div>
</template>

<script>
import ChooseStudySession from '../components/ChooseStudySession.vue';
export default {
    name: 'study-session',
    components: {
        ChooseStudySession,
    }
}
</script>