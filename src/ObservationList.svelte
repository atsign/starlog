<script lang="ts">
	import {
		Button,
		Icon,
        Modal
	} from 'sveltestrap';

    import type { ObservationModel } from './models/ObservationModel';

    import Observation from './Observation.svelte';
    import ObservationForm from './ObservationForm.svelte';
    import { ObservationStore } from './stores/observationStore';

    const dateFormatter = new Intl.DateTimeFormat('en', {
        month: 'long',
        day: 'numeric',
        year: 'numeric'
    });

    let isModalOpen = false;
    let observationStore = new ObservationStore();

    function handleObservationSave(event) {
        const newObservation: ObservationModel = event?.detail;

        if (newObservation) {
            observationStore.addObservation(newObservation);
            isModalOpen = false;
        }
    }

    function handleObservationDelete(event) {
        observationStore.deleteObservation(event.detail.observationId);
    }

    function mapObservationsToDates(observations: ObservationModel[]): Map<string, ObservationModel[]> {
        const observationMap = new Map<string, ObservationModel[]>();

        observations.map(observation => {
            const dateKey: string = dateFormatter.format(observation.dateTime);
            if (observationMap.has(dateKey)) {
                observationMap.get(dateKey).push(observation);
            } else {
                observationMap.set(dateKey, [observation]);
            }
        })

        return observationMap;
    }
</script>

<Button size="lg" color="success" style="margin: 26px 0 20px" on:click={() => isModalOpen = true}>
    <Icon name="journal-plus" /> New Observation
</Button>

<Modal body isOpen={isModalOpen}>
    <ObservationForm on:cancel={() => isModalOpen = false} on:observationSave={handleObservationSave} />
</Modal>

{#each [...mapObservationsToDates($observationStore)] as [date, observations]}
    <hr />
    <h4 class="display-6">{date}</h4>
    {#each observations as observation}
        <Observation observation={observation}
            on:observationDelete={handleObservationDelete} />
    {/each}
{:else }
    <p class="text-muted">Add some observations to get started!</p>
{/each}

<style>
    h4 {
        margin: 24px 0;
    }
</style>