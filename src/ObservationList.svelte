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

    const dateFormatter = new Intl.DateTimeFormat('en-US', {
        month: 'long',
        day: 'numeric',
        year: 'numeric'
    });

    let isModalOpen = false;
    let observationStore = new ObservationStore();
    let editId: number;

    function handleObservationSave(event) {
        const observationToSave: ObservationModel = event?.detail;

        if (observationToSave && $observationStore?.find(observation => observation.id === observationToSave.id)) {
            observationStore.editObservation(observationToSave);
        } else if (observationToSave) {
            observationStore.addObservation(observationToSave);
        }

        isModalOpen = false;
    }

    function handleObservationDelete(event) {
        observationStore.deleteObservation(event.detail.observationId);
    }

    function handleObservationEdit(event) {
        editId = event.detail.observationId;
        isModalOpen = true;
    }

    function handleCancel() {
        editId = undefined;
        isModalOpen = false;
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
    {#if isModalOpen}
    <ObservationForm on:cancel={handleCancel}
        on:observationSave={handleObservationSave}
        observationToEdit={$observationStore.find(observation => observation.id === editId)} />
    {/if}
</Modal>

{#each [...mapObservationsToDates($observationStore)] as [date, observations]}
    <hr />
    <h4 class="display-6">{date}</h4>
    {#each observations as observation}
        <Observation observation={observation}
            on:observationDelete={handleObservationDelete}
            on:observationEdit={handleObservationEdit} />
    {/each}
{:else }
    <p class="text-muted">Add some observations to get started!</p>
{/each}

<style>
    h4 {
        margin: 24px 0;
    }
</style>