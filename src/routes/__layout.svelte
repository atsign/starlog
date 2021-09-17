<script lang="typescript">
	import { goto } from '$app/navigation';
	import { isLoggedIn, refreshAuthStatus } from '$lib/stores/authStore';
	import {
		Column,
		Content,
		Grid,
		Header,
		Row,
		SideNav,
		SideNavItems,
		SideNavLink,
		SkipToContent
	} from 'carbon-components-svelte';
	import "carbon-components-svelte/css/g90.css";
	
	let isSideNavOpen = false;

	async function logoutClicked(event) {
		event.preventDefault();
		try {
			await fetch('/logout');
			await refreshAuthStatus();
			goto('/logout-success');
		} catch(err) {
			console.error(err);
		}
	}

	refreshAuthStatus();
</script>

<Header company="StarLog" bind:isSideNavOpen persistentHamburgerMenu={true}>
	<div slot="skip-to-content">
		<SkipToContent />
	</div>
</Header>

<SideNav bind:isOpen={isSideNavOpen}>
	<SideNavItems>
		<SideNavLink text="Home" href="/"></SideNavLink>
		<SideNavLink text="Observations" href="/observations"></SideNavLink>
		<SideNavLink text="About" href="/about"></SideNavLink>
		{#if ! $isLoggedIn}
		<SideNavLink text="Login" href="/login"></SideNavLink>
		{/if}
		{#if $isLoggedIn}
		<SideNavLink text="Logout" rel="external" href="/logout" on:click={logoutClicked}></SideNavLink>
		{/if}
	</SideNavItems>
</SideNav>

<Content>
	<Grid>
		<Row>
			<Column>
				<slot></slot>
			</Column>
		</Row>
	</Grid>
</Content>