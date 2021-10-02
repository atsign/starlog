<script lang="ts">
    import {
        Button,
        Modal,
        Tile,
        SkeletonPlaceholder
    } from 'carbon-components-svelte';
    import Add16 from 'carbon-icons-svelte/lib/Add16';
    import type { ObservationModel } from '$lib/models/ObservationModel';
    import { dateFormatter } from '$lib/DateTimeFormatters';
    import Observation from './Observation.svelte';
    import ObservationForm from './ObservationForm.svelte';
    import { ObservationStore, isLoading } from '$lib/stores/observationStore';


    let isModalFormOpen = false;
    let isModalConfirmOpen = false;
    let observationStore = new ObservationStore();
    let editId: number;
    let deleteId: number;
    $: observationToEdit = $observationStore.find(observation => observation.id === editId);

    function handleNewObservation() {
        isModalFormOpen = true;
        editId = undefined;
    }

    function handleObservationSave(event) {
        const observationToSave: ObservationModel = event?.detail;

        if (observationToSave && $observationStore?.find(observation => observation.id === observationToSave.id)) {
            observationStore.editObservation(observationToSave);
        } else if (observationToSave) {
            observationStore.addObservation(observationToSave);
        }

        isModalFormOpen = false;
    }

    function handleObservationDelete() {
        observationStore.deleteObservation(deleteId);
        isModalConfirmOpen = false;
    }

    function handleObservationDeleteButton(event) {
        deleteId = event.detail.observationId;
        isModalConfirmOpen = true;
    }

    function handleObservationEdit(event) {
        editId = event.detail.observationId;
        isModalFormOpen = true;
    }

    function handleObservationDeleteCancel() {
        isModalConfirmOpen = false;
        deleteId = undefined;
    }

    function handleFormCancel() {
        editId = undefined;
        isModalFormOpen = false;
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

<Modal passiveModal bind:open={isModalFormOpen} modalHeading={observationToEdit ? 'Edit observation' : 'New obsevation'}>
    {#if isModalFormOpen}
    <ObservationForm on:cancel={handleFormCancel}
        on:observationSave={handleObservationSave}
        observationToEdit={observationToEdit} />
    {/if}
</Modal>

<Modal bind:open={isModalConfirmOpen}
    danger
    modalHeading="Are you sure you want to delete this observation?"
    primaryButtonText="Delete"
    secondaryButtonText="Cancel"
    on:click:button--secondary={() => handleObservationDeleteCancel()}
    on:click:button--primary={() => handleObservationDelete()}
/>

{#if $isLoading}
<SkeletonPlaceholder />
{:else}
    <Button icon={Add16} on:click={handleNewObservation} style="margin-bottom: 40px">
        New Observation
    </Button>

    {#each [...mapObservationsToDates($observationStore)] as [date, observations]}
    <Tile>
        <h2>{date}</h2>
        {#each observations as observation}
            <Observation observation={observation}
                on:observationDelete={handleObservationDeleteButton}
                on:observationEdit={handleObservationEdit} />
        {/each}
    </Tile>
    {:else }
        <p>Add some observations to get started!</p>
    {/each}
{/if}

<style>
    h2 {
        margin-bottom: 20px;
    }
</style>