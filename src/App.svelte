<script lang="typescript">
	import "carbon-components-svelte/css/g90.css";
	import ObservationList from './ObservationList.svelte';
</script>
	
<svelte:head>
  
</svelte:head>

<header>
	<h1 class="display-4">StarLog</h1>
</header>
<div>
	<ObservationList />
</div>

<style>
	header {
		background-color: rgb(0, 0, 0);
		color: white;
		padding: 10px;
	}
</style>