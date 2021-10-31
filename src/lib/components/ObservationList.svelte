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

    interface ObservationsViewModel {
        [date: string]: ObservationModel[]
    };

    let isModalFormOpen = false;
    let isModalConfirmOpen = false;
    let observationStore = new ObservationStore();
    let editId: string;
    let deleteId: string;
    let observationsViewModel: ObservationsViewModel;
    $: observationToEdit = $observationStore.find(observation => observation.id === editId);
    $: observationsViewModel = mapObservationsToViewModel($observationStore);

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

    function mapObservationsToViewModel(observations: ObservationModel[]): ObservationsViewModel {
        const observationMap: ObservationsViewModel = {};

        observations.map(observation => {
            const dateKey: string = observation.dateTime.toISOString().split('T')[0];
            if (observationMap[dateKey]) {
                observationMap[dateKey].push(observation);
            } else {
                observationMap[dateKey] = [observation];
            }
        });

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

    {#each Object.keys(observationsViewModel).sort((a, b) => b > a ? 1 : -1) as date}
    <Tile>
        <h2>{dateFormatter.format(observationsViewModel[date][0].dateTime)}</h2>
        {#each observationsViewModel[date] as observation}
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