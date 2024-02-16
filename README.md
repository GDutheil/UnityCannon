# UnityVR Cannon Project

Membres : Robin Lotode, Guillaume Dutheil, Albane Papillon, Timothée Debon

## Setup

Ouvrir la scène Main.

Installer Text Pro Essentials quand invité à l'ouverture de la scène Main.

## Fonctionnalités présentées à la dernière séance + Remarques

* Rotation horitontale : OK
* Rotation verticale : OK mais soucis de snapping par moment
* Placement du gobelet à poudre au niveau de la mèche pour permettre de tirer mais pas de rotation du gobelet ni de particule de poudre
* Tir du canon : OK mais pas d'effet visuel
* Destruction de la cible avec collision du boulet : OK
* Réapparition automatique de la cible, placement aléatoire
* Timer avant disparition automatique de la cible (timeout)
* Système de jeu avec score, temps par cible, malus optionnel en cas de timeout (à configurer sur l'objet GameMaster dans la scène)
* Affichage du temps total écoulé, score, nombre de timeout au dessus du joueur uniquement s'il regarde vers le haut
* Affichage du temps restant avant timeout au pied du canon
* Option de rajouter un temps total avant fin de la partie (à configurer sur l'objet GameMaster dans la scène, -1 = pas de limite)

## Fonctionnalités ajoutées durant la semaine suivante

* Effet visuel de fumée quand le canon tire
* Effet visuel de poudre quand on penche le gobelet à plus de 90° : uniquement au simulateur VR, problème avec le casque
* Intéraction gobelet de poudre / canon uniquement si le gobelet est penché à plus de 90° : uniquement au simulateur VR, problème avec le casque

**IMPORTANT :** pour tester avec le casque, désactiver le simulateur dans la scène et remplacer dans *Assets/Scripts/PowderLoading.cs* ligne 29 le contenu du if par true (problème de détection de la rotation du gobelet de poudre avec le casque VR mais pas avec le simulateur)

Fichiers démonstrations dans le projet :

- Target hit.mkv
- Target miss.mkv
