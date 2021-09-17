<script lang="ts">
    import { onMount } from 'svelte';
    import { goto } from '$app/navigation';
    import { isLoggedIn, refreshAuthStatus } from '$lib/stores/authStore';
    import { Loading } from "carbon-components-svelte";
    import ObservationList from '$lib/components/ObservationList.svelte';
    
    let isLoading = true;

    onMount(async() => {
        await refreshAuthStatus();

        if ($isLoggedIn) {
            isLoading = false;
        } else {
            goto('/login');
        }
    });

</script>

{#if isLoading}
    <Loading />
{/if}

{#if $isLoggedIn}
    <ObservationList />
{/if}

<script context="module">
    export const preRender = false;
</script>