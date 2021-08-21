<script lang="ts">
    import {
        Button,
        Modal,
        Tile
    } from 'carbon-components-svelte';
    import Add16 from 'carbon-icons-svelte/lib/Add16';
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
    $: observationToEdit = $observationStore.find(observation => observation.id === editId);

    function handleNewObservation() {
        isModalOpen = true;
        editId = undefined;
    }

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

<Modal passiveModal bind:open={isModalOpen} modalHeading={observationToEdit ? 'Edit Observation' : 'New Obsevation'}>
    {#if isModalOpen}
    <ObservationForm on:cancel={handleCancel}
        on:observationSave={handleObservationSave}
        observationToEdit={observationToEdit} />
    {/if}
</Modal>

<Button icon={Add16} on:click={handleNewObservation} style="margin-bottom: 40px">
    New Observation
</Button>

{#each [...mapObservationsToDates($observationStore)] as [date, observations]}
<Tile>
    <h2>{date}</h2>
    {#each observations as observation}
        <Observation observation={observation}
            on:observationDelete={handleObservationDelete}
            on:observationEdit={handleObservationEdit} />
    {/each}        
</Tile>
{:else }
    <p>Add some observations to get started!</p>
{/each}

<style>
    h2 {
        margin-bottom: 20px;
    }
</style>