# Tower Defense Repository Reference

## Project Overview
- **Engine**: Unity (C#)
- **Primary Purpose**: Tower Defense gameplay prototype with turret interactions, enemy waves, and collectible resources (e.g., blood).

## Key Directories
- **Assets/GameLogic**: Core gameplay scripts.
- **Assets/Presentation**: Visual assets for enemies, turrets, and map.
- **Assets/Scenes**: Unity scenes for gameplay and testing.

## Notable Systems
- **TurretMerger**: Handles combining turrets and managing upgrades.
- **Spawner**: Manages enemy wave spawning.
- **GridPlacement**: Governs placement of turrets on the map.
- **Instanceables**: Contains collectible resource scripts like `Blood`.

## Interaction Guidelines
- Collectible objects typically respond to pointer events (`OnMouseEnter`, `OnMouseDown`) for user interaction.
- Turret range indicators may use colliders; ensure they do not block pointer interactions with collectibles.

## Testing Tips
- Use Unity Play Mode tests or manual scene play to verify interactions, resource collection, and turret behaviors.

*This file can be expanded as the project evolves to capture additional architecture notes and conventions.*