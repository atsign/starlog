<script lang="ts">
    import type { ObservationListModel } from "./models/ObservationListModel";
    import Observation from './Observation.svelte';

    export let observationLists: ObservationListModel[];

    const dateFormatter = new Intl.DateTimeFormat('en', {
        month: 'long',
        day: 'numeric',
        year: 'numeric'
    });
</script>

{#if observationLists }
{#each observationLists as observationList}
    <hr />
    <h4 class="display-6">{dateFormatter.format(observationList.date)}</h4>
    {#each observationList.observations as observation}
        <Observation observation={observation} />
    {/each}
{/each}
{:else }
<p class="text-muted">Add some observations to get started!</p>
{/if}

<style>
    h4 {
        margin: 24px 0;
    }
</style>